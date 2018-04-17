$(function () {
	$("input.datepicker")
		.datepicker({
			prevText: "",
			nextText: "",
			dateFormat: "dd.mm.yy"
		})
		.change(function () {
			var input = $(this);
			var id = input.data("id");
			var matchDate = input.val();	
			changePreloaderState(id, true);	
			changeErrorMessageState(false);
			$.ajax({
				type: "POST",
				url: "/MatchBasicData/Update",
				data: {	id,	matchDate },
				dataType: "json",
				success: function (data) {
					if (!data.IsSuccess)
						changeErrorMessageState(true);
					changePreloaderState(id, false);	
				},
				error: function (error) {
					changeErrorMessageState(true);
					changePreloaderState(id, false);	
				}
			});
		});

	function changeErrorMessageState(isShow) {
		changeState($("div.error-message"), isShow);
	}

	function changePreloaderState(id, isShow) {
		changeState($("div.preloader[data-id='" + id + "']"), isShow);
	}

	function changeState(input, isShow) {
		if (isShow)
			input.removeClass("hidden");
		else
			input.addClass("hidden");
	}
});