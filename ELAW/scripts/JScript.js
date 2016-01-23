// JScript File
function chkAllBox(cmb, name_f, name_b)
{
    var i = 2;
    var itxt = '00' + i;
    //alert(name_f + twonum(i) + name_b);
    while ( document.getElementById(name_f + (i < 100 ? itxt.substring(itxt.length - 2, itxt.length) : itxt) + name_b) != null)
    {
        zctl = document.getElementById(name_f + (i < 100 ? itxt.substring(itxt.length - 2, itxt.length) : itxt) + name_b)
        if (zctl.disabled == '') zctl.checked = cmb.checked;
        i++;
        itxt = (i < 100 ? '00' : '') + i;
    }
}

function ChkMinusDbl(ctl)
{
	zz = window.event.keyCode;

	if ( zz < 48 || zz > 57)  {
		if (zz == 46) {
			if (ctl.value.indexOf(".", 0) >= 0)
				window.event.keyCode = 0;
		}
		else {
			if (zz == 45) {
				if (ctl.value.indexOf("-", 0) >= 0)
					window.event.keyCode = 0;
			}
			else
				window.event.keyCode = 0;
		}			
	}
}

function ChkMinusInt(ctl)
{
    zz = window.event.keyCode;
    //alert(zz);

	if ( zz < 48 || zz > 57 )  {
		if (zz == 45) {
			if (ctl.value.indexOf('-', 0) >= 0)
				window.event.keyCode = 0;
		}
		else
			window.event.keyCode = 0;
	}
}

function DisableText(ctl)
{
    window.event.keyCode = 0;
}

function txtTime_OnKeyPress(sender) {
    var myTime = sender.value;
    if(myTime.length>4) {
        event.keyCode = 0;
        return false;
    }
    
    var charCode = (event.which) ? event.which : event.keyCode
    switch (myTime.length) {
        case 0:
            if (charCode < 48 || charCode > 50){
                event.keyCode = 0;
            }
            break;
        case 1:
            if (charCode<48||(myTime == 2&&charCode > 51)) {
                event.keyCode = 0; 
            }
            
            break;
        case 2:
            sender.value = sender.value + ':';
            break;
        case 3:
            if (charCode<48||charCode > 53) event.keyCode = 0; 
            
            break;
        default:
            if (charCode < 48 || charCode > 57) event.keyCode = 0;
    } 
}

function ValidateTime(sender) {
    if (sender.value.length == 0) return false;
    var regEx = /^(\d{2}):(\d{2})$/;
    var arrMatch = sender.value.match(regEx);
    if (arrMatch == null) {
        alert("Invalid time.");
        sender.value = "";
        return false;
    }
    var hh = arrMatch[1];
    var mm = arrMatch[2];
    if (hh >= 24 || mm >= 60) {
        alert("Invalid time.");
        sender.value = "";
        return false;
    }
    return true;
}


function CheckKeyBackSpace(){
//ป้องกันการกดปุ่ม Delete และ ปุ่ม Backspace
   var keyCode = (event.which)?event.which:event.keyCode;
    if ((keyCode == 8) || (keyCode == 46))
        event.returnValue = false;
}
