//Event delete category
$(document).ready(function(){
    $(".btn-delete-category").click(function(){
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
                      url: "/category/delete",
                      type: 'POST',
                      data: {
                          categoryId: $(this).data("categoryid"),
                      },
                      success: function(data){
                        Swal.fire({
                          icon: 'success',
                          title: 'Đã xoá thành công',
                          showConfirmButton: false,
                          timer: 2000
                        })
                        setTimeout(() => {
                          location.href = "/Category/Index"
                        }, 2000);
                      },
                      error: function(){
                        Swal.fire('Xoá thất bại')
                      }
                  });
            }});
    });
});