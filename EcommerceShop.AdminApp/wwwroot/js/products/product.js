//Event delete product
$(document).ready(function(){
  $(".btn-delete-product").click(function(){
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
            url: "/Product/Delete",
            type: "POST",
            data: {
              productId: $(this).data("productid")
            },
            success: function(data){
              Swal.fire({
                icon: 'success',
                title: 'Đã xoá thành công',
                showConfirmButton: false,
                timer: 2000
              })
              setTimeout(() => {
                location.href = window.location.href
              }, 2000);
            },
            error: function(){
              Swal.fire('Xoá thất bại')
            }
          })}
    });
  });
});

//Event role assigned
$(document).ready(function(){
    $(".btn-cate-assigned").on("click",function(){
      $.ajax({
        url: "/Product/GetCateAssigned",
            type:'POST',
            data: {
                productId : $(this).data("id"),
            },
            success: function(result){
                $("#modal-body-cateassigned").html(result);
            }
          });
        });
  });
//Clear role assigned
$(document).add(function(){
    $(document).on("click", function(event){
        if(event.target.matches("#btn-close-cateassigned"))
        {
        $("#modal-body-cateassigned").empty();
        }
    })
});