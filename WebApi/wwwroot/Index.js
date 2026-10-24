const postdata=async () => {
    const email = document.querySelector(".UserName").value;
    const firstName = document.querySelector(".FirstName").value;
    const password = document.querySelector(".Password").value;
    const lastName = document.querySelector(".LastName").value;

    const PostUser = {
        email: email, firstName: firstName, lastName: lastName, password: password
    };


    const response = await fetch('api/Users', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(PostUser)
    });
    
    if (response.ok)
    {
        alert("your sign was succsed")
        const postres = await response.json();
    }
    else
    {
        alert ("register error")

    }
    
}
const login = async () => {

    const emaillogin = document.querySelector(".UserNamelogin");
    const passwordlogin = document.querySelector(".Passwordlogin");
    const LoginUser = {
        email: emaillogin.value, password: passwordlogin.value
    };


    const response = await fetch('api/Users/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(LoginUser)
    });

    if (response.ok) {
        alert("your login was succsed")
        const postres = await response.json();
        sessionStorage.setItem("CurrentUser", JSON.stringify(postres));
    }
    else {
        alert("login error")

    }

}
