
namespace CapaInterfaz
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlError = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbDocument = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.icoMinimize = new FontAwesome.Sharp.IconPictureBox();
            this.icoMaximize = new FontAwesome.Sharp.IconPictureBox();
            this.pcbMSS = new System.Windows.Forms.PictureBox();
            this.icoExit = new FontAwesome.Sharp.IconPictureBox();
            this.icoError = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.icoShowPassword = new FontAwesome.Sharp.IconPictureBox();
            this.btnIngresar = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.btnSalir = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.pnlError.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMSS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoShowPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.panel1.CausesValidation = false;
            this.panel1.Controls.Add(this.pnlError);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.iconPictureBox1);
            this.panel1.Controls.Add(this.icoShowPassword);
            this.panel1.Controls.Add(this.btnIngresar);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txbPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txbDocument);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 750);
            this.panel1.TabIndex = 2;
            // 
            // pnlError
            // 
            this.pnlError.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnlError.BackColor = System.Drawing.Color.Brown;
            this.pnlError.Controls.Add(this.lblError);
            this.pnlError.Controls.Add(this.icoError);
            this.pnlError.Location = new System.Drawing.Point(0, 487);
            this.pnlError.Name = "pnlError";
            this.pnlError.Size = new System.Drawing.Size(442, 72);
            this.pnlError.TabIndex = 16;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Brown;
            this.lblError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblError.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblError.Location = new System.Drawing.Point(123, 16);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(297, 38);
            this.lblError.TabIndex = 11;
            this.lblError.Text = "El numero de documento o la contraseña son \r\nincorrectos, vuelva a intentarlo";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.label5.Location = new System.Drawing.Point(71, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(300, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Escriba los datos de su usuario para ingresar";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.label4.Location = new System.Drawing.Point(65, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 46);
            this.label4.TabIndex = 13;
            this.label4.Text = "¡Bievenido de vuelta!";
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.panel3.Location = new System.Drawing.Point(85, 452);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(247, 3);
            this.panel3.TabIndex = 7;
            // 
            // txbPassword
            // 
            this.txbPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.txbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbPassword.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPassword.ForeColor = System.Drawing.Color.Silver;
            this.txbPassword.Location = new System.Drawing.Point(85, 428);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(246, 20);
            this.txbPassword.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.label2.Location = new System.Drawing.Point(81, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.panel2.Location = new System.Drawing.Point(85, 369);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 3);
            this.panel2.TabIndex = 4;
            // 
            // txbDocument
            // 
            this.txbDocument.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.txbDocument.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbDocument.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDocument.ForeColor = System.Drawing.Color.Silver;
            this.txbDocument.Location = new System.Drawing.Point(85, 345);
            this.txbDocument.Name = "txbDocument";
            this.txbDocument.Size = new System.Drawing.Size(283, 20);
            this.txbDocument.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.label1.Location = new System.Drawing.Point(80, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Numero de documento";
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.pnlMensaje.Controls.Add(this.label3);
            this.pnlMensaje.Controls.Add(this.iconPictureBox2);
            this.pnlMensaje.Controls.Add(this.btnSalir);
            this.pnlMensaje.Location = new System.Drawing.Point(833, 0);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(387, 134);
            this.pnlMensaje.TabIndex = 13;
            this.pnlMensaje.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(87, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 58);
            this.label3.TabIndex = 6;
            this.label3.Text = "¿Desea salir de \r\nla aplicacion?";
            // 
            // icoMinimize
            // 
            this.icoMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.icoMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.icoMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.icoMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.icoMinimize.IconChar = FontAwesome.Sharp.IconChar.CircleMinus;
            this.icoMinimize.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.icoMinimize.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.icoMinimize.Location = new System.Drawing.Point(1112, 12);
            this.icoMinimize.Name = "icoMinimize";
            this.icoMinimize.Size = new System.Drawing.Size(32, 35);
            this.icoMinimize.TabIndex = 18;
            this.icoMinimize.TabStop = false;
            this.icoMinimize.Click += new System.EventHandler(this.icoMinimize_Click);
            // 
            // icoMaximize
            // 
            this.icoMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.icoMaximize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.icoMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.icoMaximize.ForeColor = System.Drawing.Color.DimGray;
            this.icoMaximize.IconChar = FontAwesome.Sharp.IconChar.CircleChevronUp;
            this.icoMaximize.IconColor = System.Drawing.Color.DimGray;
            this.icoMaximize.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.icoMaximize.Location = new System.Drawing.Point(1144, 12);
            this.icoMaximize.Name = "icoMaximize";
            this.icoMaximize.Size = new System.Drawing.Size(32, 35);
            this.icoMaximize.TabIndex = 15;
            this.icoMaximize.TabStop = false;
            this.icoMaximize.Click += new System.EventHandler(this.icoMaximize_Click);
            // 
            // pcbMSS
            // 
            this.pcbMSS.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbMSS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbMSS.Image = global::CapaInterfaz.Properties.Resources.MSS_logo4;
            this.pcbMSS.Location = new System.Drawing.Point(498, 25);
            this.pcbMSS.Name = "pcbMSS";
            this.pcbMSS.Size = new System.Drawing.Size(224, 77);
            this.pcbMSS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbMSS.TabIndex = 14;
            this.pcbMSS.TabStop = false;
            // 
            // icoExit
            // 
            this.icoExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.icoExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.icoExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.icoExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.icoExit.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            this.icoExit.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.icoExit.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.icoExit.Location = new System.Drawing.Point(1176, 12);
            this.icoExit.Name = "icoExit";
            this.icoExit.Size = new System.Drawing.Size(32, 35);
            this.icoExit.TabIndex = 10;
            this.icoExit.TabStop = false;
            this.icoExit.Click += new System.EventHandler(this.icoExit_Click);
            // 
            // icoError
            // 
            this.icoError.BackColor = System.Drawing.Color.Brown;
            this.icoError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.icoError.IconChar = FontAwesome.Sharp.IconChar.ExclamationCircle;
            this.icoError.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.icoError.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.icoError.IconSize = 108;
            this.icoError.Location = new System.Drawing.Point(21, -14);
            this.icoError.Name = "icoError";
            this.icoError.Size = new System.Drawing.Size(128, 108);
            this.icoError.TabIndex = 12;
            this.icoError.TabStop = false;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.HandSparkles;
            this.iconPictureBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 173;
            this.iconPictureBox1.Location = new System.Drawing.Point(55, 40);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(175, 173);
            this.iconPictureBox1.TabIndex = 14;
            this.iconPictureBox1.TabStop = false;
            // 
            // icoShowPassword
            // 
            this.icoShowPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.icoShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.icoShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.icoShowPassword.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.icoShowPassword.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.icoShowPassword.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.icoShowPassword.Location = new System.Drawing.Point(338, 426);
            this.icoShowPassword.Name = "icoShowPassword";
            this.icoShowPassword.Size = new System.Drawing.Size(32, 44);
            this.icoShowPassword.TabIndex = 10;
            this.icoShowPassword.TabStop = false;
            this.icoShowPassword.Click += new System.EventHandler(this.icoShowPassword_Click);
            // 
            // btnIngresar
            // 
            this.btnIngresar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnIngresar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnIngresar.IconColor = System.Drawing.Color.Black;
            this.btnIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnIngresar.Location = new System.Drawing.Point(84, 590);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(284, 38);
            this.btnIngresar.TabIndex = 8;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.iconPictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPictureBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            this.iconPictureBox2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconPictureBox2.IconSize = 61;
            this.iconPictureBox2.Location = new System.Drawing.Point(26, 15);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(65, 61);
            this.iconPictureBox2.TabIndex = 13;
            this.iconPictureBox2.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(96)))));
            this.btnSalir.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnSalir.IconColor = System.Drawing.Color.Black;
            this.btnSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSalir.Location = new System.Drawing.Point(26, 82);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(190, 38);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.BackgroundImage = global::CapaInterfaz.Properties.Resources.MSS_background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1220, 750);
            this.Controls.Add(this.icoMinimize);
            this.Controls.Add(this.icoMaximize);
            this.Controls.Add(this.pcbMSS);
            this.Controls.Add(this.icoExit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMensaje);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlError.ResumeLayout(false);
            this.pnlError.PerformLayout();
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMSS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoShowPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txbDocument;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnIngresar;
        private FontAwesome.Sharp.IconPictureBox icoExit;
        private FontAwesome.Sharp.IconPictureBox icoShowPassword;
        private System.Windows.Forms.Label lblError;
        private FontAwesome.Sharp.IconPictureBox icoError;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconButton btnSalir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.PictureBox pcbMSS;
        private System.Windows.Forms.Panel pnlError;
        private FontAwesome.Sharp.IconPictureBox icoMaximize;
        private FontAwesome.Sharp.IconPictureBox icoMinimize;
    }
}