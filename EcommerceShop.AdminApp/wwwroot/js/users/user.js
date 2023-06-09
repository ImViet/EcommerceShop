//Event delete user
$(document).ready(function(){
  $(".btn-delete-user").click(function(){
    Swal.fire({
      title: 'Bạn muốn xoá?',
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Xoá',
      showCancelButton: true,
      cancelButtonText: 'Huỷ'
      }).then((result) => {
        if(result.isConfirmed){
          $.ajax({
            url: "/User/Delete",
            type: 'POST',
            data: {
                userId: $(this).data("userid"),
            },
            success: function(data){
              Swal.fire({
                icon: 'success',
                title: 'Đã xoá thành công',
                showConfirmButton: false,
                timer: 2000
              })
              setTimeout(() => {
                location.href = "/User/Index"
              }, 2000);
            },
            error: function(){
              Swal.fire('Xoá thất bại')
            }
          });
      }});
  });
});

//Event detail user
$(document).ready(function(){
  $(".btn-detail-user").click(function(){
      console.log($(this).data("id"));    
      $.ajax({
          url: "/User/GetUser",
          type:'POST',
          data: {
              userId : $(this).data("id"),
          },
          success: function(result){
              $("#modal-body-detail").html(result);
              
              console.log("Ok");
          }
      });
  });
 });
 //Clear detail user
 $(document).add(function(){
  $(document).on("click", function(event){
    if(event.target.matches("#btn-close-detail"))
    {
      $("#modal-body-detail").empty();
    }
  })
 });
 
//Event role assigned
$(document).ready(function(){
  $(".btn-role-user").on("click",function(){
    $.ajax({
      url: "/User/GetRole",
          type:'POST',
          data: {
              userId : $(this).data("id"),
          },
          success: function(result){
              $("#modal-body-roleassign").html(result);
          }
        });
      });
});
 //Clear role assigned
 $(document).add(function(){
  $(document).on("click", function(event){
    if(event.target.matches("#btn-close-role"))
    {
      $("#modal-body-roleassign").empty();
    }
  })
 });