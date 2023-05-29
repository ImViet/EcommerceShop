$(document).ready(function(){
  $(".btn-update-user").click(function(){
      $.ajax({
          url: "/User/GetUser",
          type: "get",
          data: {
          userId: $(this).data("id")
          },
          success: function(data){
            $("#user-username").val(data.userName)
            $("#user-email").val(data.email)
            $("#user-password").val(data.userName)
            $("#user-confirmpassword").val(data.userName)
            $("#user-phonenumber").val(data.userName)
            $("#user-username").val(data.userName)
          }
      });
});
});
