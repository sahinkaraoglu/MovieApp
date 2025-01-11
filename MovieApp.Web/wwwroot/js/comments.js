$(document).ready(function () {
    $("#comment-form").on('submit', function (e) {
        e.preventDefault();
        
        var commentData = {
            comment: $("#commentArea").val(),
        }
        
        var url = window.location.pathname;
        var endpoint = url.includes('/movies/') ? '/movies/' : '/series/';
        var id = url.split('/')[2];
        
        $.ajax({
            url: endpoint + id + '/comment',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(commentData),
            success: function (response) {
                window.location.reload()
            },
            error: function (xhr) {
                alert('Yorum eklenirken bir hata oluştu.');
            }
        });
    });

    $('.delete-comment').click(function() {
        const commentId = $(this).data('comment-id');
        const button = $(this);
        
        if (confirm('Bu yorumu silmek istediğinize emin misiniz?')) {
            var url = window.location.pathname;
            var endpoint = url.includes('/movies/') ? '/movies/' : '/series/';
            var id = url.split('/')[2];
            
            $.ajax({
                url: endpoint + id + '/comment/' + commentId,
                type: 'DELETE',
                success: function() {
                    button.closest('.col-12').fadeOut(400, function() {
                        $(this).remove();
                    });
                },
                error: function(xhr) {
                    if (xhr.status === 401) {
                        alert('Bu yorumu silme yetkiniz yok.');
                    } else if (xhr.status === 404) {
                        alert('Yorum bulunamadı. Silinmiş olabilir.');
                        button.closest('.col-12').fadeOut(400, function() {
                            $(this).remove();
                        });
                    } else {
                        alert('Yorum silinemedi. Lütfen tekrar deneyin.');
                    }
                }
            });
        }
    });

    $('.edit-comment').click(function() {
        const commentContainer = $(this).closest('.col-12');
        const commentText = commentContainer.find('.comment-text');
        const editTextarea = commentContainer.find('.edit-comment-text');
        
        commentText.hide();
        editTextarea.show();
        $(this).hide();
        commentContainer.find('.delete-comment').hide();
        commentContainer.find('.save-comment, .cancel-edit').show();
    });

    $('.cancel-edit').click(function() {
        const commentContainer = $(this).closest('.col-12');
        const commentText = commentContainer.find('.comment-text');
        const editTextarea = commentContainer.find('.edit-comment-text');
        
        editTextarea.hide();
        commentText.show();
        commentContainer.find('.edit-comment, .delete-comment').show();
        commentContainer.find('.save-comment, .cancel-edit').hide();
        
        editTextarea.val(commentText.text().trim());
    });

    $('.save-comment').click(function() {
        const commentId = $(this).data('comment-id');
        const commentContainer = $(this).closest('.col-12');
        const commentText = commentContainer.find('.comment-text');
        const editTextarea = commentContainer.find('.edit-comment-text');
        const newText = editTextarea.val();

        var url = window.location.pathname;
        var endpoint = url.includes('/movies/') ? '/movies/' : '/series/';
        var id = url.split('/')[2];

        $.ajax({
            url: endpoint + id + '/comment/' + commentId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({ comment: newText }),
            success: function() {
                commentText.text(newText);
                
                editTextarea.hide();
                commentText.show();
                commentContainer.find('.edit-comment, .delete-comment').show();
                commentContainer.find('.save-comment, .cancel-edit').hide();
            },
            error: function(xhr) {
                if (xhr.status === 401) {
                    alert('Bu yorumu düzenleme yetkiniz yok.');
                } else if (xhr.status === 404) {
                    alert('Yorum bulunamadı. Silinmiş olabilir.');
                    commentContainer.fadeOut(400, function() {
                        $(this).remove();
                    });
                } else {
                    alert('Yorum düzenlenemedi. Lütfen tekrar deneyin.');
                }
            }
        });
    });
}); 