$(document).ready(function () {
	$(".form-select input").blur(function () {
		$(this).parent().next().removeClass("show")
		$(this).blur()
	});
});