// JScript File

//variable that will store the id of the last clicked row
var previousRow;
function ChangeRowColor(row)
{
//If last clicked row and the current clicked row are same
if (previousRow == row)
return;//do nothing
//If there is row clicked earlier
else if (previousRow != null)
//change the color of the previous row back to white
    document.getElementById(previousRow).style.backgroundColor = "#F7F7F7";

//change the color of the current row to light yellow

document.getElementById(row).style.backgroundColor = "#E7E7FF";            
//assign the current row id to the previous row id 
//for next row to be clicked
previousRow = row;
}


