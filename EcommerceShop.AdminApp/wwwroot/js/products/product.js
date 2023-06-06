$(document).ready(function(){

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