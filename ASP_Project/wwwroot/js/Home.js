var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () { 
        this.classList.toggle("active");
         
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            panel.style.display = "none";
        } else {
            panel.style.display = "block";
        }
    });
} 

//$("#searchText").on("keyup", function () {
//    var textenter = $(this).val();
//    $("table tr").each(function (results) {
//        if (results !== 0) {
//            var id = $(this).find("td:nth-child(2)").text();
//            if (id.text() !== 0 && id.toLowerCase().indexOf(textenter.toLowerCase()) < 0) {
//                $(this).hide();
//            } else {
//                $(this).show();
//            }
//        }
//    });
//});

//function updateResults() {
//    // Update hidden fields with filter values
//    $("#hiddenMinPrice").val($("#minPrice").val());
//    $("#hiddenMaxPrice").val($("#maxPrice").val());
//    $("#hiddenRAMs").val(getSelectedRAMs()); // You need to implement getSelectedRAMs() function
//    $("#hiddenSearchText").val($("#searchText").val());
//}

//function getSelectedRAMs() {
//    // Implement logic to get selected RAMs checkboxes
//    var selectedRAMs = [];
//    $("input[name='filter.RAMs']:checked").each(function () {
//        selectedRAMs.push($(this).val());
//    });
//    return selectedRAMs.join(",");
//}