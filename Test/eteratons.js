var a = [
    [-0.95, -0.06, -0.12, 0.14],
    [0.04, -1.12, 0.68, 0.11],
    [0.34, 0.08, -1.06, 0.44],
    [0.11, 0.12, 0, -1.03]
];
var r = [2.17, -1.4, 2.1, 0.8];

function base(x) {
    return [
        (r[0] - a[0][1] * x[1] - a[0][2] * x[2] - a[0][3] * x[3]) / a[0][0],
        (r[1] - a[1][0] * x[0] - a[1][2] * x[2] - a[1][3] * x[3]) / a[1][1],
        (r[2] - a[2][0] * x[0] - a[2][1] * x[1] - a[2][3] * x[3]) / a[2][2],
        (r[3] - a[3][0] * x[0] - a[3][1] * x[1] - a[3][2] * x[2]) / a[3][3]
    ];
}

function calc(ebs) {
    init();
    var res=[0, 0, 0, 0];
    var f1=form1();
    var temp;
    do
    {
        temp=arraySum(res);
        res = base(res);
    }while (Math.abs(arraySum(res)-temp)>(1-form1())/form1()*ebs);
    return res;
}

function init() {
    var k = 0;
    for(var i=0; i<2; i++){
        for(j=0; j<8; j++) {
            var t = parseFloat(document.getElementById('input'+k).value);
            a[i][j] = t;
            k++;
        }
    }
    for(var i=0; i<4; i++){
        var t = parseFloat(document.getElementById('inputb'+i).value);
        r[i] = t;
    }
}

function arraySum(a) {

    var sum = 0;
    for (var i=0; i<a.length; i++) {
        sum += a[i];
    }
    return sum;

}

function form1() {
    var sum = 0, sumt = 0;
    for (var i = 0; i<4; i++)
    {
        sumt = 0;
        for (var j=0; j<4; j++)
        {
            if (i != j)
                sumt += Math.abs(a[i][j] / a[i][i]);
        }
        if (sum <= sumt) {
            sum = sumt;
        }
    }
    return sum;
}

function showresult() {
    var p=calc(0.001);

    for(var i=0; i<4 ; i++){
        $("#answer"+i).text(p[i]);
    }
}