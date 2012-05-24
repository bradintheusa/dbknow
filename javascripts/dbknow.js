

$(document).ready(function(){
						   
	$(".NoticePane .delete").click(function(){$(this).parents(".NoticePane").animate({ opacity: 'hide' }, "slow");});
  $(".ListTable tr:even").addClass("odd");	
  
	$('textarea.commentbox').autogrow({
		maxHeight: 400,
		minHeight: 60,
		lineHeight: 16
	});  

});