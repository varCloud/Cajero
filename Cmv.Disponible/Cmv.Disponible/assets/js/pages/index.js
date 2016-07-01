var timer;
var graph;
var graph2;

function notify(style, position, titulo, mensaje) {
    if (style == "error") {
        icon = "fa fa-exclamation";
    } else if (style == "warning") {
        icon = "fa fa-warning";
    } else if (style == "success") {
        icon = "fa fa-check";
    } else if (style == "info") {
        icon = "fa fa-question";
    } else {
        icon = "fa fa-circle-o";
    }
    $.notify({
        title: titulo,
        text: mensaje,
        image: "<i class='" + icon + "'></i>"
    }, {
        style: 'metro',
        className: style,
        globalPosition: position,
        showAnimation: "show",
        showDuration: 500,
        hideDuration: 500,
        autoHide: true,
        clickToHide: true
    });
}

$(document).ready(function () {
  
});
//http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20csv%20where%20url%3D%27http%3A%2F%2Fdownload.finance.yahoo.com%2Fd%2Fquotes.csv%3Fs%3dDOW%2CNASDAQ%2CSP%26f%3Dsl1d1t1c1ohgv%26e%3D.csv%27%20and%20columns%3D%27symbol%2Cprice%2Cdate%2Ctime%2Cchange%2Ccol1%2Chigh%2Clow%2Ccol2%27&format=json&diagnostics=true&callback=