/*///////////////////////////////////////////////////////////////////////
Ported to jquery from prototype by Joel Lisenby (joel.lisenby@gmail.com)
http://joellisenby.com

original prototype code by Aarron Walter (aarron@buildingfindablewebsites.com)
http://buildingfindablewebsites.com

Distrbuted under Creative Commons license
http://creativecommons.org/licenses/by-sa/3.0/us/
///////////////////////////////////////////////////////////////////////*/

var emailEntered,
	fnameVal,
	lnameVal,
	emailtypeVal;

$(document).ready(function() {
	$("#SendButton").click(function() {
			$(".error").hide();
			var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
			var emailaddressVal = $("#email").val();

			if(emailaddressVal == '') {
				$("#message").html('<div class="alert alert-danger">Enter your email address before submitting.</div>');
				return false; 
			}
			else if(!emailReg.test(emailaddressVal)) {
				$("#message").html("<div class='alert alert-danger'>That is not an email address.&nbsp;  Typo?</div>");
				return false; 
			} 
			else {
				emailEntered = escape($('#email').val());
			}

			nameVal        = escape($("#name").val());

	});
	$('#signup').submit(function() {
		$("#message").html("<div class='alert alert-danger'>Adding your email address...</div>");
		$.ajax({
			url: 'php/mcapi-simple-subscribe-jquery/inc/store-address.php', // proper url to your "store-address.php" file
			data: 'ajax=true&email=' + emailEntered +'&name=' + nameVal,
			success: function(msg) {
				$('#message').html(msg);
			}
		});
		return false;
	});
});
