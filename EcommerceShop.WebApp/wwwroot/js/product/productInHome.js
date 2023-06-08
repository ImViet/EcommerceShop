$(document).ready(function(){
    $(".nav-item-category").click(function(){
        console.log($(this).data("categoryid")) 
        $.ajax({
            url: "/Product/GetProductByCate",
            type: "POST",
            data:{
                categoryId : $(this).data("categoryid"),
            },
            success: function(data) {
                $("#myTabContent").html(data);
            }
        });
    });
});