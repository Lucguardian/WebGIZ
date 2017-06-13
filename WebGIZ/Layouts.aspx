<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Layouts.aspx.cs" Inherits="Layouts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>WebGIZ - criando um mapa interativo em ASP.NET 2.0 usando SharpMap</title>
   <link rel="stylesheet" href="Estilo.css">
</head>
<body>
<form runat="server">
    <ul class="navbar">
      <li><a href="Default.aspx" title="Seus mapas são visualizados nesta tela.">Lousa Virtual</a>
      <li><a href="Default.aspx" title="Adiciona mapas à visualização.">Mapas</a>
      <li><a href="Tabelas.aspx" title="Permite a visualização dos dados de um mapa vetorial.">Tabela de Mapa Vetorial</a>
      <li><a href="Layouts.aspx" title="Permite a edição de um mapa a partir dos mapas carregados, para impressão.">Layouts de Impressão</a>
      <li><a href="Formulários.aspx" title="Permite criar ou preencher formulários de avaliação.">Formulários</a>
      <li><a href="Login.aspx" title="O usuário cadastrado pode carregar os mapas que possui para o servidor, bem como criar e preencher formulários.">Login</a>
    </ul>
    <div>   
    	<asp:RadioButtonList ID="rblMapTools" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" title="Com a opção Aproximar selecionada, ao clicar no mapa você aproxima a visualização.">Aproximar</asp:ListItem>
            <asp:ListItem Value="1" title="Com a opção Afastar selecionada, ao clicar no mapa você afasta a visualização.">Afastar</asp:ListItem>
            <asp:ListItem Value="2" Selected="True" title="Com a opção Mover selecionada, ao clicar no mapa você move o ponto central da visualização para o ponto onde ocorre o clique.">Mover</asp:ListItem>
        </asp:RadioButtonList>
        <asp:ImageButton Width="640" Height="480" ID="imgMap" runat="server" OnClick="imgMap_Click" style="border: 1px solid #000;" />
    </div>
</form>
Versão web do <a href="http://lucastrombinilopes.wixsite.com/softwaregiz">GIZ</a>

</body>
</html>
