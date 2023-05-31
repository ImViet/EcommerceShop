//Event delete user
$(document).ready(function(){
  $(".btn-delete-user").click(function(){
    console.log($(this).data("userid")),
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
              $(".modal-body").html(result);
              
              console.log("Ok");
          }
      });
  });
 });
