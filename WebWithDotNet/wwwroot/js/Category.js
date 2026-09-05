document.getElementById("createcategory").addEventListener("submit", async function(event)
{
    event.preventDefault();
    const form = this;
    const formData = new FormData(form);

    try{
        const response = await fetch(form.action, {
            method: "POST",
            body: formData,
            credentials: "same-origin"
        });
        const result = await response.json();
        if(result.success)
        {
           form.reset();
        }
        document.getElementById("notificationname").textContent = result.name;
        document.getElementById("notificationmessage").textContent = result.message;
        OpenNotification();
    }
    catch (error){
        document.getElementById("notificationname").textContent = "Failed";
        document.getElementById("notificationmessage").textContent = error.message;
        OpenNotification();
    }
});
function OpenNotification()
{
    document.getElementById("notification").style.display = "block";
}
function CloseNotification()
{
    document.getElementById("notification").style.display = "none";
}

function OpenEditModal(id, name, description, status)
{
    document.getElementById("editid").value = id;
    document.getElementById("editname").placeholder = name.toString();
    document.getElementById("editdescription").placeholder = description.toString();
    document.getElementById("editstatus").value = status
    document.getElementById("editmodal").style.disyplay = block;
}
function CloseEditModal(){
    document.getElementById("editmodal").style.display = none;
}
function OpenDeleteModal(id, name)
{
    document.getElementById("deletetext").textContent = "Are you sure to delete" + name.toString();
    document.getElementById("deleteid").value = id;
    document.getElementById("deletemodal").style.disyplay = block;
}
function CloseEditModal(){
    document.getElementById("deletemodal").style.display = none;
}