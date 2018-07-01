$(function () {

    //1.初始化input控件
    var oFileInput = new FileInput();
    oFileInput.Init("excel_file", "/usermanage/import");

    //2.注册导入按钮的事件
    $("#btn_import").click(function () {
        $("#myModal").modal();
    });
});

//初始化fileinput
var FileInput = function () {
    var oFile = new Object();

    //初始化fileinput控件（第一次初始化）
    oFile.Init = function (ctrlName, uploadUrl) {
        var control = $('#' + ctrlName);

        //初始化上传控件的样式
        control.fileinput({
            language: 'zh', //设置语言
            uploadUrl: uploadUrl, //上传的地址
            allowedFileExtensions: ["xls", "xlsx"], //接收的文件后缀
            showCaption: false,    //是否显示标题
            showUpload: true,      //是否显示上传按钮
            showRemove: true,      //是否显示移除按钮
            showPreview: true,     //是否显示预览按钮
            textEncoding: 'UTF-8',
            //uploadAsync: true, //默认异步上传                  
            browseClass: "btn btn-primary", //按钮样式
            allowedPreviewTypes: null,
            dropZoneEnabled: true,//是否显示拖拽区域
            //minImageWidth: 50, //图片的最小宽度
            //minImageHeight: 50,//图片的最小高度
            //maxImageWidth: 1000,//图片的最大宽度
            //maxImageHeight: 1000,//图片的最大高度
            //maxFileSize: 0,//单位为kb，如果为0表示不限制文件大小
            minFileCount: 0,
            maxFileCount: 10,//最大上传文件数限制
            enctype: 'multipart/form-data',
            validateInitialCount: true,
            previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
            previewFileIconSettings: {
                'docx': '<i ass="fa fa-file-word-o text-primary"></i>',
                'xlsx': '<i class="fa fa-file-excel-o text-success"></i>',
                'xls': '<i class="fa fa-file-excel-o text-success"></i>',
                'pptx': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
                'jpg': '<i class="fa fa-file-photo-o text-warning"></i>',
                'pdf': '<i class="fa fa-file-archive-o text-muted"></i>',
                'zip': '<i class="fa fa-file-archive-o text-muted"></i>'
            },
            uploadExtraData: {  //上传的时候，增加的附加参数
                folder: 'users', guid: $("#AttachGUID").val()
            }
        });

        //导入文件上传完成之后的事件
        $("#excel_file").on("fileuploaded", function (event, data, previewId, index) {
            //console.log(data.response);
            var result = data.response;
            if (result != null) {
                switch (result.status) {
                    case 100:
                        $("#myModal").modal("hide");
                        layer.msg(result.msg, { icon: 6, shift: 4, time: 1500 });
                        setTimeout(function () {
                            window.location.reload();
                        }, 1500);
                        break;
                    case 101:
                        $("#myModal").modal("hide");
                        layer.open({
                            type: 1, title: '导入成功提示', shade: 0.8, area: ['30%', '45%'], resize: true, moveTyppe: 1, content: '&nbsp;&nbsp;' + result.msg, btn: ['好的'], end: function () {
                                window.location.reload();
                            }
                        });
                        break;
                    case 200:
                        layer.msg(result.msg, { icon: 5, shift: 6, time: 1500 });
                        break;
                    case 201:
                        layer.open({ type: 1, title: '导入失败提示', shade: 0.8, area: ['30%', '45%'], resize: true, moveTyppe: 1, content: '&nbsp;&nbsp;' + result.msg });
                        break;
                    default:
                        break;
                }
            }
            else
                layer.msg("未知的异常，请稍后重试！", { icon: 5, shift: 6, time: 2000 });
        });
    }
    return oFile;
};