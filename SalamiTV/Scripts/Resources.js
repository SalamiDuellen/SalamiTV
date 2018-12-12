// måste vara klassnamn annars anser den att alla element i li ska räknas och togglar efter det
$(".visibleitem").click(function () {
    // enskilda elementet
    $header = $(this);
    //nästa element
    $content = $header.next();
    //togglar slidern
    $content.slideToggle(500, function () {
     //printar det som döljs vid av upslider
        $header.text(function () {
            //return $content.is(":visible") ? "Collapse" : "Expand";
        });
    });
});
// För autocomplete
function createArray() {
    var test = document.forms["topnavsearch"]["search"].value;
    var testLowCase = test.toLowerCase();
    var list = document.getElementById("tvchannel");
    var items = list.getElementsByTagName("li");
    var arr = new Array;

    for (var i = 0; i < items.length; i++) {
        var x = items[i].innerText;
        if (x.substr(6, test.length).toUpperCase() == test.toUpperCase()) {
            arr.push(x);
        }
    }
    return arr;
}
$(function () {
    var arr = createArray();
    $("#inputautocomplete").autocomplete({ source: arr });
});