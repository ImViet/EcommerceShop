$(document).ready(function(){
    $(".btn-add-cart").click(function(){
        console.log($(this).data("productid"))
         $.ajax({
             url: "/Cart/AddToCart",
             type: "POST",
             data:{
                 productId: $(this).data("productid")
             },
             success: function(data){
                 Swal.fire({
                     icon: 'success',
                     title: 'Thêm vào giỏ hàng thành công',
                     showConfirmButton: false,
                     timer: 1500
                 })
             },
             error: function(data){
                 console.log(data);
                 Swal.fire({
                     icon: 'error',
                     title: 'Vui lòng thử lại',
                     showConfirmButton: false,
                     timer: 1500
                 })
             },
         });
    });
});