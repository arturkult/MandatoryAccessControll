function main(){
    $("#loginButton").click(function (e) {
        e.preventDefault();
        email = $("input[name='login']").val();
        password = $("input[name='password']").val();
        if (password.length === 0)
            $("#errorMessage").text("Podaj hasło");
        else
            $.ajax({
                type: "POST",
                url: "Home/Login",
                data: { "email": email, "password": password },
                success: function (data) { location.replace("/"); },
                error: function (data) {
                    console.log("error");
                    $("#errorMessage").text("Błąd logowania, sprawdź login lub hasło");
                }
            });
    });
}