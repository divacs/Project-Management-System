function visiblePassword() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
} 
 

function AddProject() {  
    var assignee = document.getElementById("Assignee").value.trim();
    var name = document.getElementById("Name").value.trim();
    if (name === "" || assignee === "") { 
        if (name === "")
        {
            document.getElementById("Name").style.border = "1px solid red";
        }
        else {
            document.getElementById("Name").style.border = "";
        }
        if (assignee === "") {
            document.getElementById("Assignee").style.border = "1px solid red";
        }
        else {
            document.getElementById("Assignee").style.border = "";
        } 
        alert("Fill out red fields.") 
        event.preventDefault();
    } 

}  