function showSMS(titel, icon) {
    Swal.fire({
        position: 'top-end',//top-end
        icon: icon,
        title: titel,
        showConfirmButton: false,
        timer: 1500
    })
}
function smsAlert(sms) {
    const myArray = sms.split("-");
    if (myArray[1] == "1") {
        var titel = myArray[0];
        var icon = 'success';
        showSMS(titel, icon);
    } else {
        if (myArray[0]=='Logout') {
            login();
        } else {
            var titel = myArray[0];
            var icon = 'error';
            showSMS(titel, icon);
        }        
    }
    
}
function isEmpty(input,label) {
   // var titel = id.substring(3);
    var val = document.getElementById(input).value
    var lblText = document.getElementById(label).innerHTML;
    if (val == undefined || val.length === 0 || val.length === '') {
        showSMS(lblText + ' is require', 'warning')
        document.getElementById(input).focus();
        return true;
    } else {
        return false;
    }
}
function isEmtGridRow(id) {
    var titel = id.substring(4);
    var val = document.getElementById(id).value
    if (val == undefined || val.length === 0 || val.length === '') {
        showSMS(titel + ' is require', 'warning')
        document.getElementById(id).focus();
        return '1';
    } else {
        return '0';
    }
}
function IsJsonString(id) {
    var titel = id.substring(4);
    try {
        var json = JSON.parse(id);
        if (typeof json === 'object') {
            return '0';
        }       
    } catch (e) {
        showSMS(titel + ' is require', 'warning')
        document.getElementById(id).focus();
        return '1';
    }
}
function stringToDate(date, format, delimiter) {
    var formatLowerCase = format.toLowerCase();
    var formatItems = formatLowerCase.split(delimiter);
    var dateItems = date.split(delimiter);
    var monthIndex = formatItems.indexOf("mm");
    var dayIndex = formatItems.indexOf("dd");
    var yearIndex = formatItems.indexOf("yyyy");
    var month = parseInt(dateItems[monthIndex]);
    month -= 1;
    var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
    return formatedDate;
}
function isNumeric(str) {
    if (typeof str != "string") return false // we only process strings!  
    return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
        !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
}
function checkMobileNumber(moNumber) {
    //var mobile='01515265289';
    var pattern = /^\+?(88)?01[3456789][0-9]{8}\b/g;
    if (pattern.test(moNumber)) {
        return true;
    } else {
        return false;
    }

}
function login() {
    window.location.href = "/Login/Login";
}