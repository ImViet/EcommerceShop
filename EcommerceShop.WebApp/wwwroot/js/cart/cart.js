//Add product to cart using ajax
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

//Get mini cart
$(document).ready(function (){
    $("#btn-mini-cart").click(function(){
        $.ajax({
            url: "/Cart/GetMiniCart",
            type: "POST",
            success: function(data){
                $("#miniCart-body").html(data);
                //Hide mini cart
                $("#btn-close-miniCart").click(function(){
                    $(".offcanvas-minicart_wrapper").removeClass("open");
                    $(".global-overlay").removeClass("overlay-open");
                });
            },
            error: function(data){
                console.log("get mini cart fail");
            }
        });
    });
});

//Delete item in mini cart - using event delegation
$(document).ready(function(){
    $("#miniCart-body").on("click", ".product-item_remove", function(event){
        event.stopPropagation();
        event.preventDefault();
        $(".product-item_remove").click(function(){
            $.ajax({
                url: "/Cart/DeleteItem",
                type: "POST",
                data:{
                    productId: $(this).data("productid")
                },
                success: function(){
                    $.ajax({
                        url: "/Cart/GetMiniCart",
                        type: "POST",
                        success: function(data){
                            $("#miniCart-body").html(data);
                            //Hide mini cart
                            $("#btn-close-miniCart").click(function(){
                                $(".offcanvas-minicart_wrapper").removeClass("open");
                                $(".global-overlay").removeClass("overlay-open");
                                console.log("ok")
                            });
                            $.ajax({
                                url: "/Cart/CountCartItem",
                                type: "POST",
                                success: function(data){
                                     $("#count-cart-item").html(data)
                                }
                            });
                        },
                        error: function(data){
                            console.log("get mini cart fail");
                        }
                    })
                }
            });
        });
    });
});
