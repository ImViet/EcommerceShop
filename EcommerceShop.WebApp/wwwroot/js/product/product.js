//Add to cart open mini cart
$(document).ready(function(){
    $(".product-detail_btn-add-cart").click(function(){
        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data:{
                productId: $(this).data("productid")
            },
            success: function(data){
                $.ajax({
                    url: "/Cart/GetMiniCart",
                    type: "POST",
                    success: function(data){
                        $("#miniCart-body").html(data);
                        $(".offcanvas-minicart_wrapper").addClass("open");
                        $(".global-overlay").addClass("overlay-open");
                        //Hide mini cart
                        $("#btn-close-miniCart").click(function(){
                            $(".offcanvas-minicart_wrapper").removeClass("open");
                            $(".global-overlay").removeClass("overlay-open");
                        });
                    },
                    error: function(data){
                        console.log("get mini cart fail");
                    }
                }),
               $.ajax({
                   url: "/Cart/CountCartItem",
                   type: "POST",
                   success: function(data){
                        $("#count-cart-item").html(data)
                   }
               });
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