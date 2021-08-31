//import { signalR } from "../microsoft/signalr/dist/browser/signalr";

$(() => {

    var siteUrl = "https://localhost:44354/";

    $('#txtAmount').bind('input', function () {
        if ($(this).val() !== '') {
            $('#btnCalculate').prop('disabled', false);
        }
        else {
            $('#btnCalculate').prop('disabled', true);
        }
    });

    GetStatus();

    var signalRConnection = new signalR.HubConnectionBuilder().withUrl(siteUrl + "serverSignalR").build();
    signalRConnection.start({ withCredentials: false });
    signalRConnection.on("UpdateStatus", function () {
        GetStatus();
    });

    GetStatus();

    function GetStatus() {
        $.ajax({
            url: siteUrl + "api/Procurement/GetStatus",
            method: "GET",
            success: (result) => {
                if (result.Progress === 100) {
                    $("#status").html("Multiplication is completed, multiplied value is: <strong>" + result.Amount + "</strong>. <br>Click on the <strong>Start</strong> button to multiply the next amount.");
                    $('#btnCalculate').prop('disabled', false);
                }
                else if (result.Status === "Failed") {
                    $("#status").html("Multiplication failed, please try again or contact support team");
                    $('#btnCalculate').prop('disabled', false);
                }
                else
                    $("#status").html("Multiplication is in progress: <strong>" + result.Progress + "%</strong> completed");
            },
            error: (error) => {
                console.log(error);
            }
        })
    }
    
    $("#btnCalculate").click(function () {
        $('#btnCalculate').prop('disabled', true);
        var amount = $('#txtAmount').val();        
        const procurementAmount = { Amount: parseFloat(amount)};

        $.ajax({            
            type: "POST",            
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: "json",            
            url: siteUrl + "api/Procurement/Calculate",
            data: JSON.stringify(procurementAmount),            
            success: (result) => {
                $('#btnCalculate').prop('disabled', false);
            },
            error: (error) => {                
                console.log(error);
                $('#btnCalculate').prop('disabled', false);
            }
        })
    })
}
)