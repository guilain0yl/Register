<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" href="../../Content/default/css/register.css" />
    <script src="../../Content/default/js/jquery-1.7.1.min.js"></script>
    <script src="../../Content/default/js/Validform_v5.3.2.js"></script>
    <script src="../../Content/default/js/passwordStrength.js"></script>
    <script src="../../Content/default/js/noty/jquery.noty.js"></script>
    <script src="../../Content/default/js/noty/layouts/center.js"></script>
    <script src="../../Content/default/js/noty/themes/default.js"></script>
    <title>Register</title>
</head>
<body>
    <div class="register">
        <div class="title-bar">用户注册</div>
        <form class="userRegForm" action="Register/Validate" method="post">
            <table>
                <tr>
                    <td>用户名(邮箱):</td>
                    <td>
                        <input type="text" value="" id="username" class="c_text" datatype="e" name="UserID" errormsg="请输入有效的邮箱" /></td>
                    <td><span class="Validform_checktip">4-20个字符(英文、数字或中文)</span></td>
                </tr>
                <tr>
                    <td>密码:</td>
                    <td>
                        <input type="password" value="" plugin="passwordStrength" id="password" class="c_text" datatype="s4-20" name="UserPassword" errormsg="请输入有效的密码(4-20个字符)" />
                    </td>
                    <td><span class="Validform_checktip">4-20个字符(英文、数字或中文)</span></td>
                </tr>
                <tr>
                    <td></td>
                    <td id="#table_password">
                        <div class="passwordStrength">密码强度：<span>弱</span><span>中</span><span class="last">强</span></div></td>
                    <td></td>
                </tr>
                <tr>
                    <td>确认密码:</td>
                    <td>
                        <input type="password" class="c_text" recheck="UserPassword" datatype="*" errormsg="两次输入的密码不一致" /></td>
                    <td><span class="Validform_checktip"></span></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <label>
                            <input type="checkbox" id="chkAgree" />已阅读并接受用户协议</label></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <input id="btnReg" type="submit" value="立即注册" /></td>
                    <td></td>
                </tr>
            </table>
        </form>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".userRegForm").Validform({
                btnSubmit: "#btnReg",
                tiptype: 2,
                usePlugin: {
                    passwordstrength: {
                        minLen: 4,
                        maxLen: 20
                    }
                },
                ajaxPost:true,
                beforeSubmit: function (curform) {
                    if (!$("#chkAgree").is(":checked")) {
                        noty({ text: "对不起，没有选中协议，没法让你注册呦！", layout: "center", timeout: 5000, type: "error" });
                        return false;
                    }},
                callback: function (data) {
                    if (data.status == "y") location.href = "http://www.baidu.com";
                    else noty({ text: data.info, layout: "center", timeout: 5000 });
                }
            })
        })
    </script>
</body>
</html>
