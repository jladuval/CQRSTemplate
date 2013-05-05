// ==========================================================================
//   TOOLTIPS
// ==========================================================================
$("[rel=tooltip]").tooltip(
//                        {
//                    delay: { show: 500, hide: 100 }
//                }
);

// ==========================================================================
//   EXPANDING SESSION PANELS
// ==========================================================================
$(".collapse").collapse({
    toggle: true
});

// ==========================================================================
//   LOGIN SIGN UP
// ==========================================================================
// Exand Signup, close login
var activateForm = function (which, skipAnimation) {
    var other = which == "login" ? "signup" : "login";
    var animLength = skipAnimation ? 0 : 400;
    $(".content-" + which + "-closed").hide(0);
    $(".panel-" + which).animate({
        width: [710, 'swing']
    }, animLength);
    $(".content-" + which + "-open").show(0);
    $(".content-" + other + "-open").hide(0);
    $(".panel-" + other).animate({
        width: [210, 'swing']
    }, animLength);
    $(".content-" + other + "-closed").show(0);
    var newUrl = which == "login" ? "LogIn" : "SignUp";
    if (history.replaceState) {
        history.replaceState(null, null, newUrl);
    }
};

$(".panel-signup").click(
    function () {
        activateForm("signup");
    }
);

// Expand login, close signup
$(".panel-login").click(
    function () {
        activateForm("login");
    }
);

if ($("#startAsSignUp").val() == "value") {
    activateForm("signup", true);
}

// ==========================================================================
//   HIGHLIGHT TABLE ROW
// ==========================================================================
$(".table-summary tbody :checkbox").change(function () {
    $(this).closest("tr").toggleClass("highlight", this.checked);
});

// ==========================================================================
//   TEST FOR REMOVE FOCUS
// ==========================================================================
//$('#inputEmail').mouseleave(function () {
//    setTimeout(function (){
//        $("#inputEmail").blur();
//    }, 1000);
//});

// ==========================================================================
//   SORTABLE NESTING
// ==========================================================================
$(function () {
    // For children
    $("#sortable-child1, #sortable-child2").sortable({
        connectWith: ".connectedSortableChildren",
        placeholder: "activity-item placeholder",
        items: "li:not(activity-item new)" //disable add new ones
    }).disableSelection();

    // For Parents
    $("#sortable-parent1").sortable({
        connectWith: ".connectedSortableParents",
        placeholder: "section-item placeholder",
        items: "li:not(section-item new)" //disable add new ones
    }).disableSelection();

    // Show Move Cursor on hover
    $("#sortable-child1 li, #sortable-child2 li, #sortable-parent1 li").hover(function () {
        $(this).css('cursor', 'move');
    }, function () {
        $(this).css('cursor', 'auto');
    });
});

