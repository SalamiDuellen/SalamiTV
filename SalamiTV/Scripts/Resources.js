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