
function showresult() {
    var table=document.getElementById("main-table");
  //  console.log(table);
    var elements = table.getElementsByTagName("input");

    //var s="";
   var q, i, j;
    for ( q=0, i=0, j=0; q<elements.length; q++)
    {
       // console.log(elements[q].value);
        if ((q+1)%5==0)
        {
            r[i] = parseFloat(elements[q].value);
            i++;
            j=0;
        }
        else {
            a[i][j] = parseFloat(elements[q].value);
            j++;
        }
    }

    var p=calc(0.001);
    console.log(p[0] + "  "+  p[1] + "  "+  p[2] + "  " + p[3]);
    for(var k=0; k<4 ; k++){
        $("#answer"+k).text(p[k]);
    }
}

