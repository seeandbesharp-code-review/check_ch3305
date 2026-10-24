const currentUser = JSON.parse(sessionStorage.getItem("CurrentUser"));

if (currentUser) {
    document.querySelector(".UserName1").value = currentUser.email || "";
    document.querySelector(".FirstName1").value = currentUser.firstname || "";
    document.querySelector(".LastName1").value = currentUser.lastname || "";
    document.querySelector(".Password1").value = currentUser.password || "";
}
const update = async () => {

    const emailUp = document.querySelector(".UserName1");
    const firstNameUp = document.querySelector(".FirstName1");
    const passwordUp = document.querySelector(".Password1");
    const lastNameUp = document.querySelector(".LastName1");
    

    const currentUser = JSON.parse(sessionStorage.getItem("CurrentUser"));
    const userId = currentUser ? currentUser.id : null;
    if (!userId) {
        alert("שגיאה: משתמש אינו מחובר");
        return;
    }
    const UpdateUser = {
         id:userId ,email: emailUp.value, firstname: firstNameUp.value, lastname: lastNameUp.value, password:Number( passwordUp.value)
    };
    const response = await fetch(`api/Users/${userId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(UpdateUser)
    });

    if (response.ok) {
        alert("Update succsed")
        const postres = await response.json();
    }
    else {
        alert("Update error")

    }

}




