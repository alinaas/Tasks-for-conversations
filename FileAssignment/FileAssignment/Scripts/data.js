$(function () {
	$("input.datepicker")
		.datepicker({
			prevText: "",
			nextText: "",
			dateFormat: "dd.mm.yy"
		})
		.change(function () {
			if ($(this).val() != "") {
				$("button#saveData").prop("disabled", false);
			}
		});

	$("button#saveData").click(function () {
		var date = $("input#Date").val();
		var savedData = $("span#savedData");
		savedData.val("");
		changeErrorMessageState(false);
		$.ajax({
			type: "POST",
			url: "/Data/SaveData",
			data: { date },
			dataType: "json",
			success: function (data) {
				if (data.IsSuccess) {
					savedData.text(data.Date);
				} else {
					changeErrorMessageState(true);
				}
			},
			error: function (error) {
				changeErrorMessageState(true);
			}
		});
	});

	function changeErrorMessageState(isShow) {
		var input = $("div.error-message");
		if (isShow)
			input.removeClass("hidden");
		else
			input.addClass("hidden");
	}
});