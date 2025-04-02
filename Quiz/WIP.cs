@using Microsoft.Net.Http.Headers
@code {
    [Parameter, EditorRequired] public string Username { get; init; } = null!;
    [Parameter, EditorRequired] public string Password { get; init; } = null!;
    [Parameter] public RenderFragment? ChildContent { get; init; }
    [Parameter] public string Realm { get; init; } = "AuthRealm";
    [Inject] public IHttpContextAccessor accessor { get; init; } = default!;
    [Inject] public NavigationManager Navigation { get; init; } = default!;
    private HttpContext Context => accessor.HttpContext!;
    private bool IsSessionAuthenticated => Boolean.TryParse(Context.Session?.GetString($"{Realm}-IsAuthenticated") ?? String.Empty, out bool result);
    private bool _isAuthenticated = false;
    private bool IsAuthenticated
    {
        get
        {

            if (IsSessionAuthenticated && !_isAuthenticated)
            {
                _isAuthenticated = true;
                Context.Session.Remove($"{Realm}-IsAuthenticated");
            }
            return _isAuthenticated;
        }
    }
    private bool IsAuthorized
    {
        get
        {
            IHeaderDictionary headers = Context.Request.Headers;
            var credentials = ParseAuthorizationHeader(headers.Authorization.ToString());
            Console.WriteLine(headers.Authorization.ToString());
            return headers.ContainsKey(HeaderNames.Authorization) &&
                    credentials != null &&
                    ValidateUser(credentials.Value.username, credentials.Value.password);
        }
    }
    private string SecureArea
    {
        get
        {
            if (String.IsNullOrEmpty(Context.Session.GetString(Realm)))
                Context.Session.SetString(Realm, $"Secure-Area-{Guid.NewGuid()}");
            return (Context.Session.GetString(Realm)!);
        }
    }

    protected override void OnInitialized()
    {
        if (accessor?.HttpContext == null)
            throw new NullReferenceException($"{nameof(accessor)} service missing!");

        if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            throw new ArgumentNullException($"{nameof(Username)} or {nameof(Password)} cannot be NULL!");

        if (IsAuthorized && !IsAuthenticated)
        {
            Console.WriteLine($"DIE {IsAuthorized} -> {IsAuthenticated}");
            Context.Request.Headers.Remove(HeaderNames.Authorization);
            Context.Request.Headers.Remove(HeaderNames.WWWAuthenticate);
            Context.Session.Remove($"{Realm}-IsAuthenticated");
            _isAuthenticated = false;
        }

        Authorize();
    }

    // Authenticate the user based on the Authorization header
    private void Authorize()
    {
        if (!IsAuthenticated)
        {
            Console.WriteLine("NEED LOGIN!");
            RequestAuthentication();
            return;
        }

        _isAuthenticated = true;
        Context.Session.SetString($"{Realm}-IsAuthenticated", "true");
        Console.WriteLine("APPROVED");

    }


    // Method to trigger a login prompt
    private void RequestAuthentication()
    {
        if (!Context.Response.HasStarted)
        {
            Context.Response.Headers.CacheControl = "no-store, no-cache, must-revalidate";
            Context.Response.Headers.Pragma = "no-cache";
            Context.Response.Headers.Expires = "0";

            // Use the session-stored realm, forcing login when needed
            Context.Response.Headers.WWWAuthenticate = $"Basic realm=\"{SecureArea}\"";
            Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }

    // Parse the Basic Authorization header
    private (string username, string password)? ParseAuthorizationHeader(string authHeader)
    {
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase)) return null;

        var encodedCredentials = authHeader["Basic ".Length..].Trim();
        var credentialBytes = Convert.FromBase64String(encodedCredentials);
        var credentials = System.Text.Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

        return credentials.Length == 2 ? (credentials[0], credentials[1]) : null;
    }



    // Validate the provided username and password
    private bool ValidateUser(string username, string password) =>
        String.Equals(username, this.Username, StringComparison.Ordinal) &&
        String.Equals(password, this.Password, StringComparison.Ordinal);
}

@if (IsAuthenticated)
{
    @ChildContent
}
else
{
    <h1 class="d-flex justify-content-center align-items-center mt-5">401 Unauthorized - Authentication Required</h1>
}
