window.promptForBasicAuth = async function () {
    return new Promise((resolve) => {
        // Create a modal container
        let modal = document.createElement("div");
        modal.style.position = "fixed";
        modal.style.top = "0";
        modal.style.left = "0";
        modal.style.width = "100%";
        modal.style.height = "100%";
        modal.style.backgroundColor = "rgba(0, 0, 0, 0.5)";
        modal.style.display = "flex";
        modal.style.justifyContent = "center";
        modal.style.alignItems = "center";
        modal.style.zIndex = "1000";

        // Create a login box
        let loginBox = document.createElement("div");
        loginBox.style.backgroundColor = "white";
        loginBox.style.padding = "20px";
        loginBox.style.borderRadius = "10px";
        loginBox.style.boxShadow = "0px 0px 10px rgba(0, 0, 0, 0.3)";
        loginBox.style.display = "flex";
        loginBox.style.flexDirection = "column";
        loginBox.style.gap = "10px";

        // Create username input
        let usernameInput = document.createElement("input");
        usernameInput.type = "text";
        usernameInput.placeholder = "Enter Username";
        usernameInput.style.padding = "10px";
        usernameInput.style.border = "1px solid #ccc";
        usernameInput.style.borderRadius = "5px";

        // Create password input
        let passwordInput = document.createElement("input");
        passwordInput.type = "password"; // Hide password input
        passwordInput.placeholder = "Enter Password";
        passwordInput.style.padding = "10px";
        passwordInput.style.border = "1px solid #ccc";
        passwordInput.style.borderRadius = "5px";

        // Create login button
        let loginButton = document.createElement("button");
        loginButton.innerText = "Login";
        loginButton.style.padding = "10px";
        loginButton.style.border = "none";
        loginButton.style.backgroundColor = "#007bff";
        loginButton.style.color = "white";
        loginButton.style.borderRadius = "5px";
        loginButton.style.cursor = "pointer";

        // Handle login
        loginButton.onclick = function () {
            let username = usernameInput.value;
            let password = passwordInput.value;

            if (username && password) {
                let credentials = btoa(username + ":" + password);
                document.body.removeChild(modal); // Remove modal
                resolve(credentials);
            }
        };

        // Append elements
        loginBox.appendChild(usernameInput);
        loginBox.appendChild(passwordInput);
        loginBox.appendChild(loginButton);
        modal.appendChild(loginBox);
        document.body.appendChild(modal);
    });
};
