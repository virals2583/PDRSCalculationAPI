//import { signalR } from "../microsoft/signalr/dist/browser/signalr";

$(() => {

    GetStatus();

    var signalRConnection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44354/serverSignalR").build();
    signalRConnection.start({ withCredentials: false });
    signalRConnection.on("UpdateStatus", function () {
        GetStatus();
    });

    GetStatus();

    function GetStatus() {
        $.ajax({
            url: "https://localhost:44354/api/Procurement/GetStatus",
            method: "GET",
            success: (result) => {
                console.log(result);
                $("#status").html(result);
            },
            error: (error) => {
                console.log(error);
            }
        })
    }
    
    $("#btnCalculate").click(function () {
        $.ajax({
            url: "https://localhost:44354/api/Procurement/Calculate",
            method: "GET",
            success: (result) => {
                console.log(result);
            },
            error: (error) => {
                console.log(error);
            }
        })
    })
}
)