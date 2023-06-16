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

//Delete item in cart
$(document).ready(function(){
    $(".btn-product_remove").click(function(){
        Swal.fire({
            title: 'Bạn muốn xoá?',
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Xoá',
            showCancelButton: true,
            cancelButtonText: 'Huỷ'
            }).then((result) => {
                if(result.isConfirmed)
                {
                    $.ajax({
                        url: "/Cart/DeleteItem",
                        type: "POST",
                        data: {
                          productId: $(this).data("productid")
                        },
                        success: function(data){
                          Swal.fire({
                            icon: 'success',
                            title: 'Đã xoá thành công',
                            showConfirmButton: false,
                            timer: 1500
                          })
                          setTimeout(() => {
                            location.href = window.location.href
                          }, 1500);
                        },
                        error: function(){
                            Swal.fire('Xoá thất bại')
                        }
                    })
                }
            });
    });
});

//Prevent type in quantity
$(document).ready(function(){
    $(".cart-plus-minus-box").keydown(function (event){
        event.preventDefault();
      });
});

//Increase quantity item in cart 
$(document).ready(function(){
    $(".btn-quantity-item").click(function(){
        $.ajax({
            url: "/Cart/UpdateQuantity",
            type: "POST",
            data:{
                productId: $(this).data("productid"),
                quantity: $("#cart-item-quantity-" + $(this).data("productid")).val()
            },
            success: function(data){
                // var total = String.format("{0:0,0 vnđ}", data.total);
                $("#cart-item-amount-" + parseInt(data.id)).html(new Intl.NumberFormat().format(data.total) + " vnđ");
                $(".cart-total-payment").html(new Intl.NumberFormat().format(data.totalToPay) + " vnđ");
                $.ajax({
                    url: "/Cart/CountCartItem",
                    type: "POST",
                    success: function(data){
                         $("#count-cart-item").html(data)
                    }
                });
            }
        });
    });
});
