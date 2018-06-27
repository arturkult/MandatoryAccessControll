function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function main()
{
	console.log("main");
	$("input[value='client']").attr('checked','checked');
	$("input[name='zarejestruj']").click(function (e) {
		e.preventDefault();
		var email = $("input[name='email']").val();
		var password = $("input[name='password']").val();
        var confirmPassword = $("input[name='confirmPassword']").val();
    	var userType = $("input:checked").val();
        if(isEmail(email) && password == confirmPassword)
		{
			register(email,password,userType);
		}
		else
		{
			$("#errorMessage").text("Nieprawidłowe dane").style("color:red");
		}
	});
}

function register(email,password,userType)
{
	$.ajax({
		type:"POST",
		url: "register",
		data:{ "email":email,"password":password,"userType":userType},
		success:function(data){location.replace("index")},
		error:function (data) {
			console.log("error");
			$("#errorMessage").text(data.responseText);
        }
	})
	console.log("register out");
}


