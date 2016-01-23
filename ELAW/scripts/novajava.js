function isEnterNumber()
			{
			alert(window.event.keyCode);
				if ( ((window.event.keyCode >= 48) &&(window.event.keyCode <= 57)) || (window.event.keyCode == 46)) 
					return true;
				else
					return false;
			}
			
function isEngName()
{
	if ( ( (window.event.keyCode >= 48) &&(window.event.keyCode <= 57)  ) || (window.event.keyCode == 46) || (window.event.keyCode == 32) || (window.event.keyCode == 44) || ( (window.event.keyCode >= 65) &&(window.event.keyCode <= 90)  ) || ( (window.event.keyCode >= 97) &&(window.event.keyCode <= 122)  ) || ( (window.event.keyCode >= 40) &&(window.event.keyCode <= 41)) || (window.event.keyCode == 45) )  
		return true;
		else
	return false;
}



function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key)) return true;

    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed 
    if (key == 8 || isCtrl) {
        return true;
    }
    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

    return isFirstN || isFirstD || reg.test(keychar);
}
function blockText(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key)) return true  ;

    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed 
    if (key == 8 || isCtrl) {
        return true ;
    }
    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

    return isFirstN || isFirstD || isFirstD;
}  
   
 function Comma(number) { 
 number = '' + number;  
 var mytool_array=number.split('.');  
 var rnumber=mytool_array[0]; 
 if (rnumber.length > 3) {  
 var mod = rnumber.length % 3;  
 var output = (mod > 0 ? (rnumber.substring(0,mod)) : '');  
 for (i=0 ; i < Math.floor(rnumber.length / 3); i++) {  
 if ((mod == 0) && (i == 0))  
 output += rnumber.substring(mod+ 3 * i, mod + 3 * i + 3);  
                 else  
 output+= ',' + rnumber.substring(mod + 3 * i, mod + 3 * i + 3);  
 }  
 if (mytool_array.length>1) { 
 return (output + '.' + mytool_array[1]); } else return (output); 
 }  
 else return number;  
 }  
    
    
     function calculate(num1textbox,num2textbox,rtextbox,Operation) {  
     if ((num2textbox==null)||(rtextbox==null)) return 0; 
     //alert(num1textbox.value); 
     var A1 =num1textbox.value;  
     var B1 =num2textbox.value;  
     A1=A1.replace(/,/g, '');  
     B1=B1.replace(/,/g, '');  
     // alert(A1); 
     A = Number(A1);  
     B = Number(B1);  
     if(Operation==1) C=(A+B);  
     if(Operation==2) C=(B-A);  
     if(Operation==3) C=(A*B);  
     if(Operation==4) C=(B%A);  
     rtextbox.value= formatNumber(C,'#,.00');  
     }  
   
  
 
 