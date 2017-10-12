<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Hrff" %>
<%@ Page Language="C#"%>

<script language="cs" runat="server">

  private string result = "";
  protected void Page_Load(object sender, EventArgs e) {
    result = Common.GetTable("select * from dbo.competencies;");
  }

</script>

<%=result%>
