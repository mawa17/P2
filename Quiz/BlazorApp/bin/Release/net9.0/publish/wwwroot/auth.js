window.clearBlazorState = () => {
    console.log("Clearing Blazor session state...");
    sessionStorage.clear(); // Wipe session storage completely
};

window.globalLogout = () => {
    console.log("Logging out globally...");
    sessionStorage.clear();  // Clear Blazor state to prevent flashing
    localStorage.setItem("globalLogout", Date.now());
    window.location.reload(); // Force hard refresh
};

window.registerLogoutListener = () => {
    window.addEventListener("storage", (event) => {
        if (event.key === "globalLogout") {
            console.log("Detected logout from another tab. Reloading...");
            sessionStorage.clear(); // Wipe session state in this tab too
            window.location.reload();
        }
    });
};
