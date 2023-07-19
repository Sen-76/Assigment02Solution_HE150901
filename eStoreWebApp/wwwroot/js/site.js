function getCookie(name) {
    const cookieValue = document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)');
    if (cookieValue) {
        try {
            const decodedValue = decodeURIComponent(cookieValue[2]);
            if (decodedValue === '[]') {
                return [];
            }
            return JSON.parse(decodedValue);
        } catch (error) {
            console.error('Error parsing cookie value:', error);
        }
    }
    return [];
}


function setCookie(name, value, days) {
    const expirationDate = new Date();
    expirationDate.setTime(expirationDate.getTime() + days * 24 * 60 * 60 * 1000);
    const expires = 'expires=' + expirationDate.toUTCString();
    const encodedValue = encodeURIComponent(JSON.stringify(value));
    document.cookie = name + '=' + encodedValue + ';' + expires + ';path=/';
}

$("#logoutBtn").on('click', function (e) {
    document.cookie = "jwt=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "role=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "cart=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    window.location.href = "./login"
});