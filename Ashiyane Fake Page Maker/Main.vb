Imports System.Runtime.InteropServices
Public Class Main

    'The FindWindow API

    Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As Int32
        Return Nothing
    End Function


    Public Shared Function SetForegroundWindow(ByVal Hwnd As IntPtr) As Boolean
        Return Nothing
    End Function


    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick

        ' Find window handle

        Dim hdle As IntPtr = FindWindow(Nothing, "Security Alert")

        If hdle <> IntPtr.Zero Then

            'Bring window to front end

            SetForegroundWindow(hdle)

            ' Manage to move focus to OK or Close button

            SendKeys.Send("{TAB}")

            SendKeys.SendWait("{Enter}")

        End If



    End Sub



    Private Sub Main_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Me.CenterToScreen()
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Timer4.Interval = 1000

        Timer4.Start()

        Control.CheckForIllegalCrossThreadCalls = True
        WebBrowser1.ScriptErrorsSuppressed = False

    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Button1.Location = New Drawing.Point((GroupBox1.Size.Width / 2) - (Button1.Size.Width / 2), Button1.Location.Y)
        Button4.Location = New Drawing.Point((Me.Size.Width / 2) - (Button4.Size.Width / 2), Button4.Location.Y)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        WebBrowser1.Navigate(TextBox1.Text)
        Timer3.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If Me.WindowState <> FormWindowState.Maximized Then
            If Me.Opacity > 0 Then
                Me.Opacity -= 0.009999
            Else
                Timer1.Enabled = False
                Me.WindowState = FormWindowState.Maximized
                Me.MaximizeBox = True
                Me.CenterToScreen()
                Me.BringToFront()
                Panel2.Enabled = True
                Panel3.Visible = True
                Timer2.Enabled = True
            End If
        Else
            Timer1.Enabled = False
            Timer2.Enabled = False
        End If

    End Sub


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Me.WindowState <> FormWindowState.Normal Then

            Button1.Text = "Start Page Analyzing"

            If Me.Opacity <= 100 Then
                Me.Opacity += 0.009999
                Me.BringToFront()
            Else
                Panel3.Focus()
                Me.BringToFront()
                Panel2.Enabled = True
                Panel3.Visible = True
                Timer2.Enabled = False
            End If
        Else
            Panel2.Enabled = True
            Panel3.Visible = True
            Timer1.Enabled = False
            Timer2.Enabled = False
        End If

    End Sub

    Private Sub Main_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Me.CenterToScreen()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox3.Enabled = False
        Else
            ComboBox3.Enabled = True
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Label10.Text = "( Save Data To one Text File In Same Directory )"
        Label11.Text = "Text File Name :"
        TextBox3.Text = "Ashiyane_pass.txt"
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Label10.Text = "( Send Data With Email (doesn’t work on All server) )"
        TextBox3.Enabled = True
        Label11.Text = "Email Address :"
        TextBox3.Text = "SomeEmail@Some.Com"
    End Sub

    Dim dot As Integer = 0
    Public is_cancel As Boolean = False
    Dim _styles() As String

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        If dot <= 3 Then

            dot += 1

            If dot = 1 Then
                Button1.Text = "Loading Page."
            ElseIf dot = 2 Then
                Button1.Text = "Loading Page.."
            ElseIf dot = 3 Then
                Button1.Text = "Loading Page..."
            Else
                Button1.Text = "Loading Page...."
            End If

            If WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
                Timer1.Enabled = True
                Timer3.Enabled = False

                ComboBox1.Items.Clear()
                ComboBox2.Items.Clear()
                ComboBox3.Items.Clear()

                Label4.Text = WebBrowser1.Document.Title

                Label4.Enabled = True
                Label5.Enabled = True
                Label3.Enabled = True
                Label6.Enabled = True



                For Each get_info As HtmlElement In WebBrowser1.Document.GetElementsByTagName("FORM")
                    If get_info.GetAttribute("ACTION") <> Nothing Then
                        ComboBox3.Items.Add(get_info.GetAttribute("ACTION"))
                        ComboBox3.Text = ComboBox3.Items.Item(0)
                    End If

                    If Label5.Text.Length <= 5 Then
                        Label5.Text = get_info.GetAttribute("ACTION")
                    End If


                Next

                For Each get_inputs As HtmlElement In WebBrowser1.Document.GetElementsByTagName("INPUT")
                    If get_inputs.GetAttribute("TYPE") = "text" Then

                        If get_inputs.GetAttribute("NAME") <> Nothing Then
                            ComboBox1.Items.Add(get_inputs.GetAttribute("NAME"))
                            ComboBox1.Text = ComboBox1.Items.Item(0)
                        End If

                        If get_inputs.GetAttribute("EMAIL") <> Nothing Then
                            ComboBox1.Items.Add(get_inputs.GetAttribute("EMAIL"))
                            ComboBox1.Text = ComboBox1.Items.Item(0)
                            ComboBox2.Focus()
                        End If

                    ElseIf get_inputs.GetAttribute("TYPE") = "password" Then

                        If get_inputs.GetAttribute("NAME") <> Nothing Then
                            ComboBox2.Items.Add(get_inputs.GetAttribute("NAME"))
                            ComboBox2.Text = ComboBox2.Items.Item(0)
                        End If

                    End If
                Next
                
                Button1.Text = "Analyzing Complete"

            End If

        Else
            Button1.Text = "Loading Page"
            dot = 0
        End If
    End Sub

    Dim is_select_by_name As Boolean

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        TextBox2.Text = Nothing
        TextBox4.Text = Nothing
        Button2.Enabled = True
        Button3.Enabled = True
        is_select_by_name = False
        Button2.Visible = True
        Button3.Visible = True
        ComboBox1.Visible = False
        ComboBox2.Visible = False

    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Button2.Visible = False
        Button3.Visible = False
        TextBox2.Visible = False
        TextBox4.Visible = False
        Timer5.Enabled = False
        is_select_by_name = True
        ComboBox1.Visible = True
        ComboBox2.Visible = True
    End Sub

    Dim point_to_select As System.Drawing.Point
    Dim is_show_dialog As Boolean = False
    Private Sub Timer5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer5.Tick
        point_to_select = PointToClient(Cursor.Position) - Panel3.Location
        point_to_select.Y -= 17

        Dim at_name As String = Nothing
        If WebBrowser1.Document.GetElementFromPoint(point_to_select).TagName = "INPUT" Or WebBrowser1.Document.GetElementFromPoint(point_to_select).TagName = "input" Then
            If WebBrowser1.Document.GetElementFromPoint(point_to_select).GetAttribute("name") <> Nothing Then
                If Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
                    at_name = WebBrowser1.Document.GetElementFromPoint(point_to_select).GetAttribute("name")
                End If
            End If
        End If

        If at_name <> Nothing Then
            If inp_box = "user" And is_show_dialog = False Then
                is_show_dialog = True
                Dim msg_user = MsgBox("Are you Sure That """ & at_name & """  is UserName Box ? ", vbYesNo)
                If msg_user = MsgBoxResult.Yes Then
                    TextBox2.Text = at_name
                    is_show_dialog = False
                    Timer5.Enabled = False
                    Button3.Enabled = True
                ElseIf msg_user = MsgBoxResult.No Then
                    is_show_dialog = False
                End If
            ElseIf inp_box = "pass" And is_show_dialog = False Then
                is_show_dialog = True
                Dim msg_pass = MsgBox("Are you Sure That """ & at_name & """  is PassWord Box ? ", vbYesNo)
                If msg_pass = MsgBoxResult.Yes Then
                    TextBox4.Text = at_name
                    is_show_dialog = False
                    Timer5.Enabled = False
                    Button2.Enabled = True
                ElseIf msg_pass = MsgBoxResult.No Then
                    is_show_dialog = False
                End If
            End If
        End If
    End Sub

    Dim inp_box As String = Nothing

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        inp_box = "user"
        Timer5.Enabled = True
        TextBox2.Visible = True
        Button2.Visible = False
        Button3.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        inp_box = "pass"
        Timer5.Enabled = True
        Button3.Visible = False
        TextBox4.Visible = True
        Button2.Enabled = False
    End Sub

   
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = Nothing Then
            TextBox2.Visible = False
            Button2.Visible = True
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text = Nothing Then
            TextBox4.Visible = False
            Button3.Visible = True
        End If
    End Sub

    Function ValidateEmail(ByVal email As String) As Boolean
        Dim emailRegex As New System.Text.RegularExpressions.Regex(
            "^(?<user>[^@]+)@(?<host>.+)$")
        Dim emailMatch As System.Text.RegularExpressions.Match =
           emailRegex.Match(email)
        Return emailMatch.Success
    End Function

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim php_head As String = Nothing, user_inp, pass_inp As String
        user_inp = Nothing
        pass_inp = Nothing

        If RadioButton3.Checked Then
            If ComboBox1.Text <> Nothing Then
                user_inp = ComboBox1.Text
            End If
            If ComboBox2.Text <> Nothing Then
                pass_inp = ComboBox2.Text
            End If
        ElseIf RadioButton4.Checked Then
            If TextBox2.Text <> Nothing Then
                user_inp = TextBox2.Text
            End If
            If TextBox4.Text <> Nothing Then
                pass_inp = TextBox4.Text
            End If
        Else
            user_inp = Nothing
            pass_inp = Nothing
        End If

        If user_inp <> Nothing And pass_inp <> Nothing Then

            If RadioButton1.Checked Then

                For Each line As String In My.Resources.Save_in_Text.Split(Convert.ToChar(10))
                    If line.Contains("{0}") Then
                        php_head += line.Replace("{0}", user_inp)
                    ElseIf line.Contains("{1}") Then
                        php_head += line.Replace("{1}", pass_inp)
                    ElseIf line.Contains("{2}") Then
                        php_head += line.Replace("{2}", TextBox3.Text)
                    Else
                        php_head += line
                    End If
                Next
            ElseIf RadioButton2.Checked Then
                If ValidateEmail(TextBox3.Text) = False Then
                    MsgBox("Invalid Email Address. Check it And Try Again")
                    Exit Sub
                End If

                For Each line As String In My.Resources.Send_email.Split(Convert.ToChar(10))
                    If line.Contains("{0}") Then
                        php_head += line.Replace("{0}", user_inp)
                    ElseIf line.Contains("{1}") Then
                        php_head += line.Replace("{1}", pass_inp)
                    ElseIf line.Contains("{2}") Then

                        php_head += line.Replace("{2}", TextBox3.Text)
                    Else
                        php_head += line
                    End If
                Next
            End If
        End If
        MsgBox(php_head)
    End Sub
End Class
