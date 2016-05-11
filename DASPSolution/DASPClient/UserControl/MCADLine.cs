using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

using System.Collections.Generic;

namespace Dasp
{



    public delegate void UpdateMouseValue(int x,int y,float tim,float yvalue);
	/// <summary>
	/// 
	/// </summary>
	public class MCADLine : System.Windows.Forms.UserControl
    {
        #region 2016
        /// <summary>
        /// �������Ԥ���ı�ע�ռ�
        /// </summary>
        public int Lspan
        {
            set { lspan = value; }
            get { return lspan; }
        }
        /// <summary>
        /// ���÷ֲ���������
        /// </summary>
        /// <param name="indextstart"></param>
        /// <param name="len"></param>
        /// <param name="indexpart"></param>
        public void SetDrawAera(int indexpart,int indextstart,int len )
        {
            if ((LVP != null) && (indexpart < this.LVP.Count))
            {
                if (indextstart < LVP[indexpart].DrawDatalstPara[0].Count)
                LVP[indexpart].Sindex = indextstart;

                if (indextstart + len < LVP[indexpart].DrawDatalstPara[0].Count)
                {
                    LVP[indexpart].Eindex = indextstart + len - 1;
                }
                else
                    LVP[indexpart].Eindex = -1;

                this.Refresh();
            }
             
        }
        public string[] RightText
        {
            set;
            get;
        }
        public int Rspan
        {
            set { rspan = value; }
            get { return rspan; }
        }
        #region ����ʹ��
        public int sindex
        {
            set;
            get;
        }
        public int drawlen
        {
            set;
            get;
        }
        public void SetDrawPara(int index,int dcount)
        {
            sindex = index;
            drawlen = dcount;
            this.Refresh();
        }
        #endregion ����ʹ��
        /// <summary>
        /// ��ǰ�ؼ�����������Ϣ
        /// </summary>
        private List<ViewPartInfo> LVP = new List<ViewPartInfo>();
        /// <summary>
        /// ��ֱͼ�η�������
        /// </summary>
        private int drawparts = 1;
        public int DrawParts
        {
            set { drawparts = value; }
            get { return drawparts; }
        }
        private List<int> Heights = new List<int>();
        /// <summary>
        /// ����������Ļ��Ƹ߶�,ע��ȡ������
        /// </summary>
        private void CalculatePartDrawHeight()
        {
            // this.Heights.Clear();
            if (LVP.Count != this.drawparts) //�����������������ı�
            {
                LVP.Clear();
                ViewPartInfo viewPartInfo = null;
                for (int i = 0; i < drawparts; i++)
                {
                    viewPartInfo = new ViewPartInfo();
                    LVP.Add(viewPartInfo);
                }
            }
            //int unith = this.Height / drawparts;
            //int remand = this.Height % drawparts;

            //for (int i = 0; i < remand; i++)
            //{
            ////    Heights.Add(unith + 1);
            //    LVP[i].dHeight = unith + 1;
            //   // Heights[i] = unith + 1;
            //}
            //for (int i = remand; i < drawparts; i++)
            //{
            //    LVP[i].dHeight = unith ;
            // //   Heights.Add(unith);
            //}
            int m = 0;
            for (int i = 0; i < drawparts; i++)
            {
                m = m + LVP[i].percent;
            }
            float unith = 1f * this.Height / m;

            for (int i = 0; i < drawparts; i++)
            {
                LVP[i].dHeight = Convert.ToInt32(unith * LVP[i].percent);
                // Heights.Add(unith);
            }
        }
        /// <summary>
        /// ����������Ļ��Ƹ߶�,ע��ȡ������
        /// </summary>
        private void CalculatePartDrawHeight(IList<int> scape)
        {
            this.Heights.Clear();
            if (LVP.Count != this.drawparts) //�����������������ı�
            {
                LVP.Clear();
                ViewPartInfo viewPartInfo = null;
                for (int i = 0; i < drawparts; i++)
                {
                    viewPartInfo = new ViewPartInfo();
                    LVP.Add(viewPartInfo);
                }
            }
            int m = 0;
            for (int i = 0; i < drawparts; i++)
            {
                m = m + LVP[i].percent;
            }
            float unith = 1f * this.Height / m;

            for (int i = 0; i < drawparts; i++)
            {
                LVP[i].dHeight = Convert.ToInt32(unith * LVP[i].percent);
                // Heights.Add(unith);
            }
        }
        /// <summary>
        /// �������еĻ�������,��ռ�û�������Ŀռ����
        /// </summary>
        /// <param name="datalsts"></param>
        public void SetDrawDataAll(IList<IList<float>[]> datalsts, IList<int> scape)
        {
            int tmpparts = datalsts.Count;//��ȡ������������
            DrawParts = tmpparts;//���û�����������
            ViewPartInfo viewPartInfo = null;
            LVP.Clear();
            for (int i = 0; i < drawparts; i++)
            {
                viewPartInfo = new ViewPartInfo();
                viewPartInfo.DrawDatalstPara = datalsts[i];//����ÿ��������Ļ�������
                viewPartInfo.percent = scape[i];
                LVP.Add(viewPartInfo);
            }
            CalculatePartDrawHeight();
            exd = true;
            this.Refresh();
        }

        /// <summary>
        /// �������еĻ�������,��ռ�û�������Ŀռ����
        /// </summary>
        /// <param name="datalsts"></param>
        public void SetDrawDataAll(IList<IList<float>[]> datalsts, IList<int> scape, IList<DataBase> dbex)
        {
            int tmpparts = datalsts.Count;//��ȡ������������
            DrawParts = tmpparts;//���û�����������
            ViewPartInfo viewPartInfo = null;
            LVP.Clear();
            for (int i = 0; i < drawparts; i++)
            {
                viewPartInfo = new ViewPartInfo();
                viewPartInfo.DrawDatalstPara = datalsts[i];//����ÿ��������Ļ�������
                viewPartInfo.percent = scape[i];
                viewPartInfo.dbase = dbex[i];
                LVP.Add(viewPartInfo);
            }
            CalculatePartDrawHeight();
            exd = true;
            this.Refresh();
        }

        /// <summary>
        /// �����ƶ�����Ļ��Ʋ���
        /// </summary>
        /// <param name="indexpart">�����������</param>
        /// <param name="datalst">��ͼ��������</param>
        public void SetDrawDataofPart(int indexpart, IList<float>[] datalst)
        {
            if ((LVP != null) && (indexpart < LVP.Count))
            {
                LVP[indexpart].DrawDatalstPara = datalst;
            }

        }
        /// <summary>
        /// �������еĻ�������
        /// </summary>
        /// <param name="datalsts"></param>
        public void SetDrawDataAll(IList<IList<float>[]> datalsts)
        {
            int tmpparts = datalsts.Count;//��ȡ������������
            DrawParts = tmpparts;//���û�����������
            ViewPartInfo viewPartInfo = null;
            LVP.Clear();
            for (int i = 0; i < drawparts; i++)
            {
                viewPartInfo = new ViewPartInfo();
                viewPartInfo.DrawDatalstPara = datalsts[i];//����ÿ��������Ļ�������
                LVP.Add(viewPartInfo);
            }
            CalculatePartDrawHeight();
            exd = true;
            this.Refresh();
        }
        /// <summary>
        /// ʹ����չ�㷨����ͼ��
        /// </summary>
        private bool exd = false;
     
        public UpdateMouseValue updateMouseValue = null;
        #endregion 2016
        #region 2011
        private string strTitle = "ʵʱ��"; //����
        private Color clrBgColor = Color.Snow; //����ɫ
        private Color clrTextColor = Color.White; //������ɫ
        private Color clrBorderColor = Color.Black; //����߿���ɫ
        private Color clrAxisColor = Color.Gray; //������ɫ
        private Color clrSliceTextColor = Color.Black; //�̶�������ɫ
        private float fltYRotateAngle = 0f; //Y��������ת�Ƕ�
        /// <summary>
        /// ����ͼ�����ߵ���ɫ
        /// </summary>
        private Color clrsCurveColors = Color.Red;

        /// <summary>
        ///�Ƚ�ͼ�����ߵ���ɫ
        /// </summary>
        private Color clrsCurveColorsb = Color.Green;
        private Color clrSliceColor = Color.Black; //�̶���ɫ
        private int intFontSize = 9; //�����С����
        private int intCurveSize = 1; //����������С
        /// <summary>
        /// �����������
        /// </summary>
        public int CurveSize
        {
            get
            {
                return intCurveSize;
            }
            set
            {
                intCurveSize = value;
            }
        }
        /// <summary>
        /// �����С����
        /// </summary>
        public int FontSize
        {
            get
            {
                return intFontSize;
            }
            set
            {
                intFontSize = value;
            }
        }
        private System.ComponentModel.Container components = null;
        private Panel ChartPanel;
        /// <summary>
        /// ���ƵĻ�������
        /// </summary>
        private Graphics g;
        /// <summary>
        /// �ж��Ƿ���Ի��ƣ�û�����ݻ�ֻ��һ�����ݵ�Ͳ���Ҫ����
        /// </summary>
        private bool CanDraw
        {
            get
            {
                return canDraw();
            }
        }
      

        /// <summary>
        /// ���⣬�Ѿ�ʹ��
        /// </summary>
        public string Title
        {
            set
            {
                strTitle = value;
            }
            get
            {
                return strTitle;
            }
        }
        /// <summary>
        /// ����ɫ���Ѿ�ʹ��
        /// </summary>
        public Color BgColor
        {
            set
            {
                clrBgColor = value;
            }
            get
            {
                return clrBgColor;
            }
        }
        /// <summary>
        /// ���ƿ̶�������ɫ
        /// </summary>
        public Color TextColor
        {
            set
            {
                clrTextColor = value;
            }
            get
            {
                return clrTextColor;
            }
        }
        /// <summary>
        /// ���û��ƻ���ͼ���ߵ���ɫ
        /// </summary>
        public Color ClrsCurveColors
        {
            set { clrsCurveColors = value; }
            get {return clrsCurveColors;}
        }
        /// <summary>
        /// ���ñȽ�ͼ�����ߵ���ɫ
        /// </summary>
        public Color ClrsCurveColorsb
        {
            set { clrsCurveColorsb = value; }
            get { return clrsCurveColorsb; }
        }
        /// ������ɫ
        /// </summary>
        public Color AxisColor
        {
            set
            {
                clrAxisColor = value;
            }
            get
            {
                return clrAxisColor;
            }
        }
        /// <summary>
        /// ����߿���ɫ���Ѿ�ʹ��
        /// </summary>
        public Color BorderColor
        {
            set
            {
                clrBorderColor = value;
            }
            get
            {
                return clrBorderColor;
            }
        }
        /// <summary>
        /// �̶���ɫ
        /// </summary>
        public Color SliceColor
        {
            set
            {
                clrSliceColor = value;
            }
            get
            {
                return clrSliceColor;
            }
        }
        /// <summary>
        /// �̶�������ɫ
        /// </summary>
        public Color SliceTextColor
        {
            set
            {
                clrSliceTextColor = value;
            }
            get
            {
                return clrSliceTextColor;
            }
        }
        /// <summary>
        /// ����̶���Ҫ��ʾ��С��λ��
        /// </summary>
        private int decima = 1;
        public int Decima
        {
            set
            {
                decima = value;
            }
            get
            {
                return decima;
            }
        }
        /// <summary>
        /// ��ȡ�����û��ƶ���֮��ļ��
        /// </summary>
        public int barDiffWidth
        {
            set
            {
                BarDiff = value;
                Calculatea();
            }
            get
            {
                return BarDiff;
            }
        }
        /// <summary>
        /// ���ƶ���֮��ļ��������Ϊ��λ
        /// </summary>
        private int BarDiff = 4;//
        /// <summary>
        /// ��ȡ�����û��ƶ���֮��Ŀ�϶��� ��϶���+���ƿ�ȵ��ڵ�λ���ƶ���ռ�õĿ��
        /// </summary>
        public int barWidth
        {
            set
            {
                PerBarWith = value;
                Calculatea();
            }
            get
            {
                return PerBarWith;
            }
        }
        /// <summary>
        /// ���ƶ���Ŀ��  �����ӵ�����ȣ������ر�ʾ
        /// </summary>
        private int PerBarWith = 2;
        /// <summary>
        /// ����һ�����Ƶ�
        /// </summary>
        /// <param name="barp"></param>
        public void AddNewDate(double barp)
        {

            this.Refresh();
        }
        /// <summary>
        /// ��ͼ����
        /// </summary>
        private Size CurrentSize = new Size(0, 0);
        private float fltYSpace = 0f; //ͼ�����¾����Ե����
        float[] dashValues = { 3, 3 };
        /// <summary>
        /// ��ȡͼ��������Ե����
        /// </summary>
        public int XSpace
        { get { return this.innerbar; } set { this.innerbar = value; } }
        /// <summary>
        /// ͼ�����¾����Ե����
        /// </summary>
        public float YSpace
        {
            get
            {
                return fltYSpace;
            }
            set
            {
                fltYSpace = value;
            }
        }
      
       
        /// <summary>
        /// ����ʾ����������
        /// </summary>
        private int DisPlayBars = 0;
        /// <summary>
        /// ���㵱ǰ��������ʾ�Ļ�������
        /// ���õĻ�����Χ���Ե�λ���Ƶ�Ŀ�ȣ����ƶ�����+���ƶ���֮��Ŀ�϶��
        /// �ó��ɱ�ʾ���Ƶ���
        /// </summary>
        private void Calculatea()
        {
            DisPlayBars = (Width - rspan - innerbar) / (this.PerBarWith + BarDiff);
        }
        /// <summary>
        /// ����Ŀǰ��λ�������ƵĶ�����
        /// </summary>
        /// <returns></returns>
        public int SCA()
        {
            int sc = 1;
            switch (BarDiff)
            {
                case 22://ÿ��λ����ʾ1��k������� 22���� 10
                    sc = 1;
                    break;
                case 8:  //ÿ��λ����ʾ2����������� 8����
                    sc = 2;
                    break;
                case 4:  //ÿ��λ����ʾ4����������� 4����
                    sc = 4;
                    break;
                case 2: //ÿ��λ����ʾ8����������� 2����2
                    sc = 8;
                    break;
                case 1:
                    sc = 16;//ÿ��λ����ʾ8����������� 1����1
                    break;
                case 0:
                    sc = 32;//ÿ��λ����ʾ32����������� 0����1
                    break;
                default:
                    break;
            }
            return sc;

        }
        
        /// <summary>
        /// ��ǰ��������ɷֱ��������
        /// </summary>
        /// <returns></returns>
        private int MaxBar()
        {
            int ti;
            ti = this.Width - this.rspan - this.innerbar; //�����������ظ�����û�м��
            //
            ti = ti * scale / this.Gridspan;
            return ti;
        }
        /// <summary>
        /// Y��������ת�Ƕ�
        /// </summary>
        public float YRotateAngle
        {
            get
            {
                return fltYRotateAngle;
            }
            set
            {
                fltYRotateAngle = value;
            }
        }
       
        /// <summary>
        /// ��̶����ڻ����еĸ߶�
        /// </summary>
        private int ZeroLeve = 400;      
        /// <summary>
        /// X����ࣨ���ظ�����
        /// </summary>
        private float fltXSlice = 50; 
        #endregion
        #region ��ͼ����
       
       
        #endregion 
        /// <summary>
		/// ��������߼��ϱߵĿ�϶ 
		/// </summary>
		private int innerbar = 4;
		/// <summary>
		/// �ױ�����עʱ������Ŀռ�
		/// </summary>
		private int bmspan = 4;

        public int BmSpan
        {
            set { bmspan = value; }
            get { return bmspan; }
        }
        /// <summary>
        /// �������϶�Ԥ����ע���߶�
        /// </summary>
        private int topremark = 50;
        public int TopRemarkSpan
        {
            set { topremark = value; }
            get { return topremark; }
        }
        /// <summary>
		/// Y��������ߴ��࣬������Ϊ��λ
		/// </summary>
		private int Gridspan = 20;
        /// <summary>
        /// �����߶ȣ�Y��������ߴ��ࣩ
        /// </summary>
        public int yGridspan
        {
            set {Gridspan = value;}
            get { return Gridspan; }
        }       
   		
      
		/// <summary>
		/// �ұ�����ע����Ŀռ�
		/// </summary>
		private int rspan = 50;
        

        /// <summary>
        /// �������ע����Ŀռ�
        /// </summary>
        private int lspan = 50;
	
       
        /// <summary>
        /// ��֤Y��Ŀ̶���100������
        /// </summary>
        /// <param name="_dv"></param>
        /// <param name="wdth"></param>
        /// <returns></returns>
        private double GetInt(double _dv, int wdth)
        {
            double dvalue = 0f;
          
           dvalue =  Convert.ToInt32(_dv / wdth );
            return dvalue;
        }
		
        /// <summary>
        /// ָʾ��ǰ�Ƿ���Խ���ͼ�λ���
        /// </summary>
        /// <returns></returns>
        private bool canDraw()
        {
            bool cdraw = true;
            if (exd)
            {
                cdraw = false;
                if (LVP != null)
                {
                    cdraw = true;
                }
            }
          
            return cdraw;
        }
		
      
		/// <summary>
		/// X��Ļ��ƶ���֮��ļ������ֵ���������ƶ�������Ŀ�ȣ���СΪ1
        ///һ�����ӵĿ�ȳ���һ�����ӱ�ʶ�ĵ���  =   ÿ���������ֵ  ����С�ǵ����֮��Ϊһ������
        ///����ֱ��ָ����Ҳ���԰���λ���ӷ��õĵ���ȷ��
		/// </summary>
		public float XSlice
		{	
            set 
            { 
                fltXSlice = value; 
            }	
            get 
            { 
                return fltXSlice; 
            }	
        } 		
		/// <summary>
		/// ��ȡ��������̶����ڻ����еĸ߶ȣ�������Ϊ��λ
		/// </summary>
		public int  ZeroL
		{
			get 
            {
                return ZeroLeve;
            } 
            set 
            {
                ZeroLeve = value;
            }
		}		
        /// <summary>
        ///Y�ᵥλ�̶ȵ�����ʾ����ֵ 
        /// </summary>
		private float fltYSliceValue = 20;
        /// <summary>
        /// Y����ʼ�̶�����ʾ��ֵ
        /// </summary>
		private float fltYSliceBegin = 30; 

        /// <summary>
        /// Y�ᶥ������ʾ��ֵ
        /// </summary>
        private float fltYSliceEnd = 30;
		/// <summary>
		/// ���û��ȡY���λ�̶ȵ�����ʾ����ֵ,��Ӧ��ȷ����?
		/// </summary>
		public float YSliceValue
		{	
            set 
            { 
                fltYSliceValue = value; 
            }		
            get 
            { 
                return fltYSliceValue;
            }		
        }
		/// <summary>
		/// Y��̶ȿ��
		/// </summary>
		public float YSlice
		{	
            set 
            { 
                fltYSlice = value;
            }		
            get
            { 
                return fltYSlice;
            }	
        }
		private float fltYSlice = 50; //Y��̶ȿ��
		/// <summary>
		/// Y��ײ�����ʾ��ֵ����ͼ���ϱ�ʾ����Сֵ��
		/// </summary>
		public float YSliceBegin
		{	
            set 
            { 
                fltYSliceBegin = value;
            }		
            get 
            {
                return fltYSliceBegin;
            }		
        }
        /// <summary>
        /// Y�ᶥ��������ʾ��ֵ����ͼ���ϱ�ʾ�����ֵ��
        /// </summary>
        public float YSliceEnd
        {
            set
            {
                fltYSliceEnd = value;
            }
            get
            {
                return fltYSliceEnd;
            }
        }
		/// <summary>
		/// ÿ�����ʾ�ĵ���
		/// </summary>
		private int scale = 10; //
        #region �Ŵ���С�����Ȳ���


      
		
        /// <summary>
        /// �����֮��ļ����������Ϊ��λ
        /// </summary>
        public int xInterval
        {
            set;
            get;
        }
     
     
        #endregion 
       
       
      
        ///// <summary>
        ///// ÿҳ�ɻ��Ƶĵ���
        ///// </summary>
        //private int pageNumberOfValue = 1024;
        ///// <summary>
        ///// ���û��Ƶ���
        ///// </summary>
        //public int PageNumberOfValue
        //{
        //    set 
        //    {
        //        if (value == -1)
        //        {
        //            drawall = true;
        //        }
        //        else
        //        {
        //            drawall = false;
        //            pageNumberOfValue = value;
        //        }
        //        this.Refresh();
        //    }
        //    get 
        //    {
        //        return pageNumberOfValue;
        //    }
        //}

        /// <summary>
        /// �ȽϷ���ָʾ
        /// </summary>
        private bool cmp = false;
       
       
        /// <summary>
        /// �����ڵ�ǰ����ÿ�����Ƶ�������£�������ܹ��ɻ��Ƶ�����
        /// �Ա��ڽ��з�ҳ����
        /// </summary>
        /// <returns></returns>
        //private int GetPagePoint()
        //{
        //    //int xnum = (this.Width - this.rspan - innerbar) / (this.xGridSpan);//�ɻ�������̶�����
        //    //pageNumberOfValue = xnum * xUnitPointNum;//ÿҳ�ɻ��Ƶĵ��� 
        //    if ((pageNumberOfValue > 0) && (this.CurrentNumberOfValues < pageNumberOfValue))
        //        pageNumberOfValue = CurrentNumberOfValues;//����ҳ���Ƶĵ��������ܵ���ʱ  ÿҳ���Ƶ��������ܵ���
        //    int drwaPageTotalPages = this.CurrentNumberOfValues / pageNumberOfValue;
        //    if ((Remainder = this.CurrentNumberOfValues % pageNumberOfValue) > 0)
        //   {
        //       DrwaPageTotalPages = drwaPageTotalPages + 1;
        //   }
        //   else
        //   {
        //       DrwaPageTotalPages = drwaPageTotalPages;
        //   }
        //   return DrwaPageTotalPages;
        //}
        /// <summary>
        /// ����
        /// </summary>
        public int Remainder
        {
            set;
            get;
        }
        public MCADLine()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            YSliceBegin = -2000;
            this.YSliceEnd = 2000;
            xGridSpan = 64;
            xUnitPointNum = 64;
            yGridspan = 100;
            Khz = 25600;
            sindex = 0;
            drawlen = 1024;
            //pageNumberOfValue = 1024;
          // this.PreProcessControlMessage
           
            imgTemp = new Bitmap(this.ChartPanel.Width, this.ChartPanel.Height);
            //imgTemp = new Bitmap(200, 200, ChartPanel.CreateGraphics());
            //g = Graphics.FromImage(imgTemp);
            g = ChartPanel.CreateGraphics();
            DrawChart();
        }
      
       
        private Bitmap imgTemp = null;//new BitMap(200, 200, g);//����Image���󣬴�СΪ200 * 200
      
        /// <summary>
        /// ����������
        /// </summary>
        public int StartPindex
        {
            set;
            get;
        }
        /// <summary>
        /// �����յ����
        /// </summary>
        public int EndPindex
        {
            set;
            get;
        }
        /// <summary>
        /// ����ͼ��ҳ��
        /// </summary>
        public int DrawPageIndex
        {
            set;
            get;
        }
        /// <summary>
        /// ��ǰ���Ƶ���ҳ�� =  �����ܵ���  /��ǰ��Ļ���ܻ��Ƶĵ���
        /// ��ǰ��Ļ���ܻ��Ƶĵ��� = ��Ļ�Ļ��ƿ�� / ÿ����ռ�õĺ�������������
        /// </summary>
        public int DrwaPageTotalPages
        {
            set;
            get;
        }
        /// <summary>
        /// Y�ᵥλ�����أ�����ʾ��ֵ
        /// </summary>
        private int unitgridyvalue
        {
            set;
            get;
        }
        /// <summary>
        /// ���ú�������Ŀ�� ,������Ϊ��λ 
        /// </summary>
        public int xGridSpan
        {
            set;
            get;
        }
        /// <summary>
        ///��λ����Ԫ�������Ƶĵ���
        /// ����ԽСͼ��Խ���൱�ں���Ŵ󣬵�չʾ�ĵ���Խ��
        /// ��ǰ��������Ŀ���ɶ��ٸ���λ��Ԫ��
        /// </summary>
        public int xUnitPointNum
        {
            set;
            get;
        }
        private int khz;
        /// <summary>
        /// ����Ƶ��
        /// </summary>
        public int Khz
        {
            set { khz = value; this.Refresh(); }
            get { return khz; }
        }
		
       /// <summary>
       /// Ԥ�����������y������Ĵ�С�Լ���λ��������ʾ��ֵ
       /// </summary>
       /// <param name="indexpart">���Ƶķ������</param>
        public void Fit(int indexpart)
        {
            if ((LVP != null) && (indexpart < this.LVP.Count))
            {

                float fltMaxValue = LVP[indexpart].maxValue;
                int tmph = innerbar + topremark;//ֻ�и������
                if (LVP[indexpart].dataStyle == DataStyle.nplusp)
                {
                    tmph = (int)((this.LVP[indexpart].dHeight - bmspan - innerbar - topremark) * fltMaxValue / (fltMaxValue - this.LVP[indexpart].minValue)) + innerbar + topremark;//�����������������߸߶�
                }
                else if (LVP[indexpart].dataStyle == DataStyle.onlypos)
                {
                    tmph = this.LVP[indexpart].dHeight - bmspan;//ֻ����ֵ���֣���̶����ڵײ�Ԥ����϶֮��
                }
                LVP[indexpart].y_zeroHeght = tmph;
                if (LVP[indexpart].dataStyle == DataStyle.nplusp)  //�ڻ��ƿռ��������и�ֵ����£�ȷ������ĺ����Сֵ
                {

                    int  ynlentop = (LVP[indexpart].y_zeroHeght - 1 * this.innerbar - topremark) / this.Gridspan;//�ȼ���ϵͳĬ������߶������Ϊy�����Ϸ�����������
                    int  ynlenbottom = (LVP[indexpart].dHeight - bmspan - LVP[indexpart].y_zeroHeght) / this.Gridspan;//��ֵ������ܵ���������
                    int ynlen = Math.Min(ynlentop, ynlenbottom);
                    if (ynlen < 1)  //����һ��������Ҫ����ϵͳ�趨�������С
                    {
                        if (ynlen == ynlentop)  //���ϲ�����Ϊ׼���������С
                        {
                            LVP[indexpart].yGridSnap = LVP[indexpart].y_zeroHeght - 1 * this.innerbar - topremark;
                        }
                        else
                        {
                            LVP[indexpart].yGridSnap = LVP[indexpart].dHeight - bmspan - LVP[indexpart].y_zeroHeght;
                        }
                        ynlen = 1;
                    }
                    else
                    {
                        LVP[indexpart].yGridSnap = this.Gridspan;
                    }

                    float f100 = GetSacle(Math.Max(Math.Abs(LVP[indexpart].maxValue), Math.Abs(LVP[indexpart].minValue)));

                    unitgridyvalue = Convert.ToInt32((Math.Max(Math.Abs(LVP[indexpart].maxValue), Math.Abs(LVP[indexpart].minValue)) / (f100 * ynlen)) * f100);//unitgridyvalue ��ʾy���ϵ�λ��̶��ߵ�ֵ���˴���ʾ�黯Ϊ10�ı���
                  
                    LVP[indexpart].y_ValuePerPixel = 1f * unitgridyvalue / LVP[indexpart].yGridSnap;  //�����ÿ�����ظ��������ֵ
                }
            }
        }
        private  float GetSacle(float fValue)
        {
            float scale = 1f;

            string b = fValue.ToString("E8");
            int b1 = b.IndexOf('E');
            string c = b.Substring(b1 + 1, 1);
            string d = b.Substring(b1 + 2, 3);
            int d1 = Convert.ToInt32(d);
            int c1 = 1;
            if (c.Equals("-"))
            {
                c1 = -1;
                scale = Convert.ToSingle(Math.Pow(10, c1 * (d1 + 1)));
            }
            else
            {
                scale = Convert.ToSingle(Math.Pow(10, c1 * (d1 - 1)));
            }
            return scale;
        }
       
     
     
	
		/// <summary>
		/// �����ƶ���ı��⣬�����Ͻǻ���
		/// </summary>
		/// <param name="objGraphics"></param>
		private void DrawTitle()
		{
			g.DrawString(Title, new Font("����", FontSize), new SolidBrush(TextColor), new Point((int)this.innerbar  , (int)(YSpace + FontSize)));
		}
     
       
        public int GetValidDrawWith
        {
            get { return this.rspan + innerbar; }
        }
        /// <summary>
        /// ��λ���ػ��Ƶĵ���,ȫ����ʾʱ,���ܼ�������һ��
        /// </summary>
        private float unitPoints
        {
            set;
            get;
        }
       
        /// <summary>
        /// �ڷ�����һ���ϻ������е�ʱX��Ļ���
        /// </summary>
        /// <param name="objGraphics"></param>
        private void DrawAllXAxis(int indexparts)
        {
            Pen penDashed = new Pen(new SolidBrush(AxisColor));
            penDashed.DashStyle = DashStyle.Custom;
            penDashed.DashPattern = dashValues;
            int xnum = (this.Width - this.rspan - this.lspan - innerbar) / LVP[indexparts].xGridSnap;// x�����Ͽɻ����������
            string strSliceText = string.Empty;
            g.DrawLine(penDashed, this.Width - this.rspan, SumHeght(indexparts) + innerbar + topremark, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);
            float unittime = LVP[indexparts].x_grids* LVP[indexparts].xValueScale; //���ᵥλ�����ʾ��ֵ
            for (int i = 0; i <= xnum; i++)
            {
                g.DrawLine(penDashed, innerbar + i * LVP[indexparts].xGridSnap + this.lspan, SumHeght(indexparts) + innerbar + topremark, innerbar + i * LVP[indexparts].xGridSnap + this.lspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);
                strSliceText = (i * unittime).ToString("f2");//��ǰ��������ʾ��Yֵ���̶���
                //g.TranslateTransform(this.XSpace + i * this.xGridSpan, /*this.YSliceBegin*/ this.ZeroL); //ƽ��ͼ��(ԭ��)
                //g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                //g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Red /*SliceTextColor*/), 0, 0);
                //g.ResetTransform(); //����ͼ�� 

                g.TranslateTransform(this.XSpace + this.lspan + i * LVP[indexparts].xGridSnap, /*this.YSliceBegin*/SumHeght(indexparts) + LVP[indexparts].dHeight - 1* bmspan); //ƽ��ͼ��(ԭ��)
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(clrTextColor /*SliceTextColor*/), 0, 0);
                g.ResetTransform(); //����ͼ�� 
            }
            if (this.Mousex != -1)
            {
                g.DrawLine(penDashed, this.Mousex, SumHeght(indexparts) +innerbar + this.topremark, this.Mousex, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);
                this.DrawTopRemark(indexparts);
            }
            penDashed.Dispose();
        }
        private void DrawTopRemark(int indexparts)
        {
            string txtcontext = "";
            int i =Convert.ToInt32( MouseLocation * LVP[indexparts].DisplayLen());//.DrawDatalstPara[0].Count);
            if (i < LVP[indexparts].DisplayLen()) //.DrawDatalstPara[0].Count)
            {
                txtcontext = Convert.ToString(LVP[indexparts].videoValue[i].xTime) + " V:" + Convert.ToString(LVP[indexparts].videoValue[i].yN_Mss);
                g.TranslateTransform(this.XSpace + this.lspan, SumHeght(indexparts) + 12); //ƽ��ͼ��(ԭ��)
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(txtcontext, new Font("����", FontSize), new SolidBrush(clrTextColor /*SliceTextColor*/), 0, 0);
                g.ResetTransform();
            }
        }
	    /// <summary>
	    /// �ڷ�����ȫ����������
	    /// </summary>
	    /// <param name="indexparts"></param>
        public void DrawAllContent(int indexparts)
        {
            Pen CurvePen = new Pen(clrsCurveColors, CurveSize);
            Pen CurvePen2 = new Pen(clrsCurveColorsb, CurveSize);
            GraphicsPath m_gp = new GraphicsPath();
            Brush mbrush = new LinearGradientBrush(new Rectangle(0, 0, this.barWidth, this.barWidth), Color.Green, Color.Green, LinearGradientMode.Horizontal);
            PointF[] CurvePointF = new PointF[LVP[indexparts].DrawDatalstPara[0].Count];//һϵ����������
            PointF[] CurvePointF2 = new PointF[LVP[indexparts].DrawDatalstPara[0].Count];//һϵ����������
            float keys = 0;
            float values = 0;
            m_gp.Reset();
            float unitv = Convert.ToSingle(this.Width - this.rspan - innerbar - this.lspan) / Convert.ToSingle(LVP[indexparts].DrawDatalstPara[0].Count);//ÿ����ռ��X��Ŀ�� 
            if (LVP[indexparts].DrawGroup != 1) //cmp)
            {
                LVP[indexparts].videoValue.Clear();
                LVP[indexparts].videoValueb.Clear();
                for (int i = 0; i < LVP[indexparts].DrawDatalstPara[0].Count; i++)
                {
                    keys = unitv * i + XSpace + this.lspan;
                   // if (LVP[indexparts].DrawDatalstPara[0][i] > 0)
                    {
                        values = SumHeght(indexparts) +  (LVP[indexparts].y_zeroHeght) - (float)(LVP[indexparts].DrawDatalstPara[0][i] / LVP[indexparts].y_ValuePerPixel);//�˴���ת������������ֵ,
                        CurvePointF[i] = new PointF(keys, values);

                        values = SumHeght(indexparts) + (LVP[indexparts].y_zeroHeght) - (float)(LVP[indexparts].DrawDatalstPara[1][i] / LVP[indexparts].y_ValuePerPixel);//�˴���ת������������ֵ,
                        CurvePointF2[i] = new PointF(keys, values);

                        VideoValue _VideoValue = new VideoValue(i, (float)(LVP[indexparts].DrawDatalstPara[0][i]));
                        LVP[indexparts].videoValue[i] = _VideoValue;
                        _VideoValue = new VideoValue(i, (float)(LVP[indexparts].DrawDatalstPara[1][i]));
                        LVP[indexparts].videoValueb[i] = _VideoValue;
                    }
                   

                }
               
              
                g.FillRegion(mbrush /*UnderBrush*/, new Region(m_gp));
                g.DrawLines(CurvePen, CurvePointF);
                g.DrawLines(CurvePen2, CurvePointF2);
            }
            else
            {
                LVP[indexparts].videoValue.Clear();
                for (int i = 0; i < LVP[indexparts].DrawDatalstPara[0].Count; i++)  //ֻ��һ�����ݣ�ֻ����һ��
                {
                    keys = unitv * i + XSpace + this.lspan;
                    // if (LVP[indexparts].DrawDatalstPara[0][i] > 0)
                    {
                        values = SumHeght(indexparts)+(LVP[indexparts].y_zeroHeght) - (float)(LVP[indexparts].DrawDatalstPara[0][i] / LVP[indexparts].y_ValuePerPixel);//�˴���ת������������ֵ,
                        CurvePointF[i] = new PointF(keys, values);
                    }
                    VideoValue _VideoValue = new VideoValue(i, (float)(LVP[indexparts].DrawDatalstPara[0][i]));
                    LVP[indexparts].videoValue[i] = _VideoValue;
                }
                g.FillRegion(mbrush /*UnderBrush*/, new Region(m_gp));
                g.DrawLines(CurvePen, CurvePointF);
            }
            mbrush.Dispose();
            CurvePen.Dispose();
            CurvePen2.Dispose();
        }
        /// <summary>
        /// �ۼƵ�ǰ�������ο�����߶�
        /// </summary>
        /// <param name="indexparts"></param>
        /// <returns></returns>
        private int SumHeght(int indexparts)
        {
            int sumh = 0;
            if (indexparts > 0)
            {
                for (int i = 0; i < indexparts ; i++)
                {
                    sumh += LVP[i].dHeight;
                }
            }
            return sumh;
        }
        /// <summary>
        /// ����ȫ���ǻ���Y�����߼������ϵĿ̶Ⱥ�����,���ҳ�����޹�
        /// </summary>
        /// <param name="objGraphics"></param>
        private void DrawAllYAxis(int indexparts)
        {
            string strSliceText = string.Empty;
            Pen penDashed = new Pen(new SolidBrush(AxisColor));
            //g.DrawLine(penDashed, this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght, this.Width - innerbar - this.rspan, SumHeght(indexparts)+ LVP[indexparts].y_zeroHeght);//��0�����
            //g.DrawLine(penDashed, this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan, this.Width - innerbar - this.rspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);//���Ƶ�ǰ�������ĵײ�����
            penDashed.DashStyle = DashStyle.Custom;
            penDashed.DashPattern = dashValues;
            int ynlen = (LVP[indexparts].y_zeroHeght - 1 * this.innerbar - topremark) / LVP[indexparts].yGridSnap;//y�����Ϸ��������
            // int  unitgridyvalue = Convert.ToInt32(this.YSliceEnd /(10* ynlen ))*10;//��ʾy���ϵ�λ���ʾ��ֵ
            for (int i = 0; i <= ynlen; i++)
            {	//���������ߣ���0�������ϻ���				
                g.DrawLine(penDashed, this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght - i * LVP[indexparts].yGridSnap, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght - i * LVP[indexparts].yGridSnap);  //�����������ϻ�������

                strSliceText = Convert.ToString(LVP[indexparts].yGridSnap * LVP[indexparts].y_ValuePerPixel * i);//��ʾ�����ߵĿ̶�ֵ
                //strSliceText = Convert.ToString(unitgridyvalue * i);
                g.TranslateTransform(this.Width - this.rspan, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) - i * LVP[indexparts].yGridSnap - FontSize / 2); //��ע�Ҳ�̶���
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                g.ResetTransform();
                g.TranslateTransform(this.XSpace, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) - i * LVP[indexparts].yGridSnap - FontSize / 2); //��ע���̶���
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);

                g.ResetTransform(); //����ͼ�� 
            }
            ynlen = (LVP[indexparts].dHeight - bmspan - LVP[indexparts].y_zeroHeght/*this.ZeroL*/ ) / LVP[indexparts].yGridSnap;// this.Gridspan;//y�����·����������
            for (int i = 0; i <= ynlen; i++)
            {	//���������ߣ���0�������»���						
                g.DrawLine(penDashed, this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght + i * LVP[indexparts].yGridSnap, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght + i * LVP[indexparts].yGridSnap);
                strSliceText = Convert.ToString(-LVP[indexparts].yGridSnap * LVP[indexparts].y_ValuePerPixel * i + 0);//��ǰ��������ʾ��Yֵ���̶���
                // strSliceText = Convert.ToString(-unitgridyvalue * i);
                g.TranslateTransform(this.Width - this.rspan, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) + i * LVP[indexparts].yGridSnap - FontSize / 2); //ƽ��ͼ��(ԭ��)
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                g.ResetTransform(); //����ͼ�� 

                g.TranslateTransform(this.XSpace, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) + i * LVP[indexparts].yGridSnap - FontSize / 2); //ƽ��ͼ��(ԭ��)
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                g.ResetTransform(); //����ͼ�� 
            }
            penDashed.Dispose();
        }
      
        private string TopRemark = "";
        /// <summary>
        /// ����̧ͷ��ע��Ϣ
        /// </summary>
        private void DrawTopRemark(string txtcontext)
        {
            g.TranslateTransform(this.XSpace + this.lspan ,  5); //ƽ��ͼ��(ԭ��)
            g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
            g.DrawString(txtcontext, new Font("����", FontSize), new SolidBrush(clrTextColor /*SliceTextColor*/), 0, 0);
            g.ResetTransform(); //����ͼ�� 
        }
    

    
		
        public bool drawall
        {
            set;
            get;
        }
        /// <summary>
        /// ���Ҳ��ע�й�����
        /// </summary>
        private void RemarLeftText()
        {
           
           if (RightText != null)
            // int  unitgridyvalue = Convert.ToInt32(this.YSliceEnd /(10* ynlen ))*10;//��ʾy���ϵ�λ���ʾ��ֵ
            for (int i = 0; i < RightText.Length; i++)
            {	//���������ߣ���0�������ϻ���				
               
                g.TranslateTransform(this.Width - this.rspan +5,  this.topremark + i *FontSize *2 - FontSize / 2); //��ע�Ҳ�̶���
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(RightText[i], new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                g.ResetTransform();

            }
           

        }
        /// <summary>
        /// ����ʵʩ ˢ�»�����ʱ���� 
        /// </summary>
        public void DrawChart()
        {
            CalculatePartDrawHeight();
            g.FillRectangle(new SolidBrush(BgColor), 1, 1, Width - 2, Height);//- 2); //���߿�����
            //g.DrawRectangle(new Pen(BorderColor, 1), this.innerbar + this.lspan, this.innerbar + topremark, Width - rspan - this.innerbar - this.lspan, Height -  this.innerbar - topremark - bmspan); //���߿�
         //   xInterval = 1;
            if (CanDraw)
            {
              

                if (exd) //���÷�������ͼ�λ���
                {
                    RemarLeftText();
                    for (int i = 0; i < this.drawparts; i++)  //���λ��Ƹ���������ͼ��
                    {
                        bool br = false;
                        if (br)
                        {
                            Fit(i);

                            if (drawall) //�������е�
                            {
                                LVP[i].x_unitPoints = 1f * LVP[i].DrawDatalstPara[0].Count / (this.Width - this.rspan - innerbar - this.lspan);  //��λ���ر�ʾ�ĵ���.
                                LVP[i].x_grids = Convert.ToInt32(LVP[i].x_unitPoints * LVP[i].DrawDatalstPara[0].Count / (this.Width - this.rspan - innerbar - this.lspan));
                                LVP[i].xGridSnap = this.xGridSpan;
                                DrawAllYAxis(i);
                                DrawAllXAxis(i);
                                DrawAllContent(i);
                            }
                        }
                        else
                        {
                            ReCalculateLVP(LVP[i].Sindex,  LVP[i].DisplayLen(), i);
                            //ReCalculateLVP(sindex, drawlen, i);
                            Fit_ext(i);

                            //if (drawall) //�������е�
                            //{Convert.ToInt32
                                LVP[i].x_unitPoints = 1f * LVP[i].DisplayLen() / (this.Width - this.rspan - innerbar - this.lspan);  //��λ���ر�ʾ�ĵ���.
                                LVP[i].x_grids = (LVP[i].x_unitPoints * LVP[i].DisplayLen() / (this.Width - this.rspan - innerbar - this.lspan));
                                LVP[i].xGridSnap = this.xGridSpan;
                                DrawAllYAxis_ext(i);
                                DrawAllXAxis_ext(i);//����
                                DrawAllContent_ext(i);
                            //}
                        }
                    }
                }

               
               
            }
        }
        #region �ƶ���������
        /// <summary>
        /// ָ�����λ���ͼ��
        /// </summary>
        /// <param name="indextstart">�������</param>
        /// <param name="len">���Ƴ���</param>
        /// <param name="indexpart"></param>
        private void ReCalculateLVP(int indextstart,int len,int indexpart)
        {
            if ((LVP != null) && (indexpart < this.LVP.Count))
            {
                LVP[indexpart].ChangeStartNum_Len(indextstart, len);//�����μ���������ݵ������Сֵ
            }

        }
        public void Fit_ext(int indexpart)
        {
            if ((LVP != null) && (indexpart < this.LVP.Count))
            {

                float fltMaxValue = LVP[indexpart].maxValue;
                int tmph = innerbar + topremark;//ֻ�и������
                if (LVP[indexpart].dataStyle == DataStyle.nplusp)
                {
                    tmph = (int)((this.LVP[indexpart].dHeight - bmspan - innerbar - topremark) * fltMaxValue / (fltMaxValue - this.LVP[indexpart].minValue)) + innerbar + topremark;//�����������������߸߶�
                }
                else if (LVP[indexpart].dataStyle == DataStyle.onlypos)
                {
                    tmph = this.LVP[indexpart].dHeight - bmspan;//ֻ����ֵ���֣���̶����ڵײ�Ԥ����϶֮��
                }
                LVP[indexpart].y_zeroHeght = tmph;
                if (LVP[indexpart].dataStyle == DataStyle.nplusp)  //�ڻ��ƿռ��������и�ֵ����£�ȷ������ĺ����Сֵ
                {

                    int ynlentop = (LVP[indexpart].y_zeroHeght - 1 * this.innerbar - topremark) / this.Gridspan;//�ȼ���ϵͳĬ������߶������Ϊy�����Ϸ�����������
                    int ynlenbottom = (LVP[indexpart].dHeight - bmspan - LVP[indexpart].y_zeroHeght) / this.Gridspan;//��ֵ������ܵ���������
                    int ynlen = Math.Min(ynlentop, ynlenbottom);
                    if (ynlen < 1)  //����һ��������Ҫ����ϵͳ�趨�������С
                    {
                        if (ynlen == ynlentop)  //���ϲ�����Ϊ׼���������С
                        {
                            LVP[indexpart].yGridSnap = LVP[indexpart].y_zeroHeght - 1 * this.innerbar - topremark;
                        }
                        else
                        {
                            LVP[indexpart].yGridSnap = LVP[indexpart].dHeight - bmspan - LVP[indexpart].y_zeroHeght;
                        }
                        ynlen = 1;
                    }
                    else
                    {
                        LVP[indexpart].yGridSnap = this.Gridspan;
                    }

                    float f100 = GetSacle(Math.Max(Math.Abs(LVP[indexpart].maxValue), Math.Abs(LVP[indexpart].minValue)));

                    unitgridyvalue = Convert.ToInt32((Math.Max(Math.Abs(LVP[indexpart].maxValue), Math.Abs(LVP[indexpart].minValue)) / (f100 * ynlen)) * f100);//unitgridyvalue ��ʾy���ϵ�λ��̶��ߵ�ֵ���˴���ʾ�黯Ϊ10�ı���

                    LVP[indexpart].y_ValuePerPixel = 1f * unitgridyvalue / LVP[indexpart].yGridSnap;  //�����ÿ�����ظ��������ֵ
                }
            }
        }
        private void DrawAllYAxis_ext(int indexparts)
        {
            string strSliceText = string.Empty;
            Pen penDashed = new Pen(new SolidBrush(AxisColor));
          
            penDashed.DashStyle = DashStyle.Custom;
            penDashed.DashPattern = dashValues;
            int ynlen = (LVP[indexparts].y_zeroHeght - 1 * this.innerbar - topremark) / LVP[indexparts].yGridSnap;//y�����Ϸ��������
            // int  unitgridyvalue = Convert.ToInt32(this.YSliceEnd /(10* ynlen ))*10;//��ʾy���ϵ�λ���ʾ��ֵ
            for (int i = 0; i <= ynlen; i++)
            {	//���������ߣ���0�������ϻ���				
                g.DrawLine(penDashed, this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght - i * LVP[indexparts].yGridSnap, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght - i * LVP[indexparts].yGridSnap);  //�����������ϻ�������

                strSliceText = Convert.ToString(LVP[indexparts].yGridSnap * LVP[indexparts].y_ValuePerPixel * i);//��ʾ�����ߵĿ̶�ֵ
                //strSliceText = Convert.ToString(unitgridyvalue * i);
                //g.TranslateTransform(this.Width - this.rspan, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) - i * LVP[indexparts].yGridSnap - FontSize / 2); //��ע�Ҳ�̶���
                //g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                //g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                //g.ResetTransform();
                g.TranslateTransform(this.XSpace, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) - i * LVP[indexparts].yGridSnap - FontSize / 2); //��ע���̶���
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);

                g.ResetTransform(); //����ͼ�� 
            }
            ynlen = (LVP[indexparts].dHeight - bmspan - LVP[indexparts].y_zeroHeght/*this.ZeroL*/ ) / LVP[indexparts].yGridSnap;// this.Gridspan;//y�����·����������
            for (int i = 0; i <= ynlen; i++)
            {	//���������ߣ���0�������»���						
                g.DrawLine(penDashed, this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght + i * LVP[indexparts].yGridSnap, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].y_zeroHeght + i * LVP[indexparts].yGridSnap);
                strSliceText = Convert.ToString(-LVP[indexparts].yGridSnap * LVP[indexparts].y_ValuePerPixel * i + 0);//��ǰ��������ʾ��Yֵ���̶���
                // strSliceText = Convert.ToString(-unitgridyvalue * i);
                //g.TranslateTransform(this.Width - this.rspan, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) + i * LVP[indexparts].yGridSnap - FontSize / 2); //ƽ��ͼ��(ԭ��)
                //g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                //g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                //g.ResetTransform(); //����ͼ�� 

                g.TranslateTransform(this.XSpace, LVP[indexparts].y_zeroHeght + SumHeght(indexparts) + i * LVP[indexparts].yGridSnap - FontSize / 2); //ƽ��ͼ��(ԭ��)
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
                g.ResetTransform(); //����ͼ�� 
            }
            #region ��עY�ᵥλ
            strSliceText = Convert.ToString(LVP[indexparts].dbase.yName);//��ǰ��������ʾ��Yֵ��λ


            g.TranslateTransform(this.XSpace + this.lspan, SumHeght(indexparts) + this.innerbar + topremark - FontSize - 2); //ƽ��ͼ��(ԭ��)
            g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
            g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Gold /*SliceTextColor*/), 0, 0);
            g.ResetTransform(); 
            #endregion //����ͼ�� 
            g.DrawLine(new Pen(BorderColor, 1), this.lspan + innerbar, SumHeght(indexparts) + this.innerbar + topremark, this.Width - this.rspan, SumHeght(indexparts) + this.innerbar + topremark);//���ϲ� 
            g.DrawLine(new Pen(BorderColor, 1), this.lspan + innerbar, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);//���Ƶײ�����
            penDashed.Dispose();
        }
        /// <summary>
        /// �ڷ�����һ���ϻ������е�ʱX��Ļ���
        /// </summary>
        /// <param name="objGraphics"></param>
        private void DrawAllXAxis_ext(int indexparts)
        {
            Pen penDashed = new Pen(new SolidBrush(AxisColor));
            penDashed.DashStyle = DashStyle.Custom;
            penDashed.DashPattern = dashValues;
            int xnum = (this.Width - this.rspan - this.lspan - innerbar) / LVP[indexparts].xGridSnap;// x�����Ͽɻ����������
            string strSliceText = string.Empty;
           
            float unittime = LVP[indexparts].x_grids * LVP[indexparts].xValueScale; //���ᵥλ�����ʾ��ֵ
            for (int i = 0; i <= xnum; i++)
            {
                g.DrawLine(penDashed, innerbar + i * LVP[indexparts].xGridSnap + this.lspan, SumHeght(indexparts) + innerbar + topremark, innerbar + i * LVP[indexparts].xGridSnap + this.lspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);
                strSliceText =( LVP[indexparts].Sindex + (i * unittime)).ToString("f2")  ;//��ǰ��������ʾ��Yֵ���̶���
                //g.TranslateTransform(this.XSpace + i * this.xGridSpan, /*this.YSliceBegin*/ this.ZeroL); //ƽ��ͼ��(ԭ��)
                //g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                //g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(Color.Red /*SliceTextColor*/), 0, 0);
                //g.ResetTransform(); //����ͼ�� 

                g.TranslateTransform(this.XSpace + this.lspan + i * LVP[indexparts].xGridSnap, SumHeght(indexparts) + LVP[indexparts].dHeight + FontSize/2  - bmspan); //ƽ��ͼ��(ԭ��)
                g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
                g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(clrTextColor /*SliceTextColor*/), 0, 0);
                g.ResetTransform(); //����ͼ�� 
            }
            if (this.Mousex != -1)
            {
                g.DrawLine(penDashed, this.Mousex, SumHeght(indexparts) + innerbar + this.topremark, this.Mousex, SumHeght(indexparts) + LVP[indexparts].dHeight -  bmspan );
                this.DrawTopRemark(indexparts);
            }
            #region ��ע�������굥λ
            strSliceText =  LVP[indexparts].dbase.xName;
            g.TranslateTransform(this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].dHeight - 1 * bmspan - FontSize); //ƽ��ͼ��(ԭ��)
            g.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //��תͼ��
            g.DrawString(strSliceText, new Font("����", FontSize), new SolidBrush(clrTextColor /*SliceTextColor*/), 0, 0);
            g.ResetTransform();
            #endregion
            g.DrawLine(new Pen(BorderColor, 1), this.Width - this.rspan, SumHeght(indexparts) + innerbar + topremark, this.Width - this.rspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);
            g.DrawLine(new Pen(BorderColor, 1), innerbar + this.lspan, SumHeght(indexparts) + innerbar + topremark, innerbar + this.lspan, SumHeght(indexparts) + LVP[indexparts].dHeight - bmspan);
            penDashed.Dispose();
        }
        /// <summary>
        /// �ڷ�����ȫ����������
        /// </summary>
        /// <param name="indexparts"></param>
        public void DrawAllContent_ext(int indexparts)
        {
            Pen CurvePen = new Pen(clrsCurveColors, CurveSize);
            Pen CurvePen2 = new Pen(clrsCurveColorsb, CurveSize);
            GraphicsPath m_gp = new GraphicsPath();
            Brush mbrush = new LinearGradientBrush(new Rectangle(0, 0, this.barWidth, this.barWidth), Color.Green, Color.Green, LinearGradientMode.Horizontal);
            PointF[] CurvePointF = new PointF[LVP[indexparts].DisplayLen()];//.DrawDatalstPara[0].Count];//һϵ����������
            PointF[] CurvePointF2 = new PointF[LVP[indexparts].DisplayLen()];//.DrawDatalstPara[0].Count];//һϵ����������
            float keys = 0;
            float values = 0;
            m_gp.Reset();
            float unitv = Convert.ToSingle(this.Width - this.rspan - innerbar - this.lspan) / Convert.ToSingle(LVP[indexparts].DisplayLen());//ÿ����ռ��X��Ŀ�� 
            if (LVP[indexparts].DrawGroup != 1) //������������Ҫ���бȽ�
            {
                LVP[indexparts].videoValue.Clear();
                LVP[indexparts].videoValueb.Clear();
                for (int i = LVP[indexparts].Sindex; i < LVP[indexparts].DisplayLen(); i++)
                {
                    keys = unitv * i + XSpace + this.lspan;
                    // if (LVP[indexparts].DrawDatalstPara[0][i] > 0)
                    {
                        values = SumHeght(indexparts) + (LVP[indexparts].y_zeroHeght) - (float)(LVP[indexparts].DrawDatalstPara[0][i] / LVP[indexparts].y_ValuePerPixel);//�˴���ת������������ֵ,
                        CurvePointF[i] = new PointF(keys, values);

                        values = SumHeght(indexparts) + (LVP[indexparts].y_zeroHeght) - (float)(LVP[indexparts].DrawDatalstPara[1][i] / LVP[indexparts].y_ValuePerPixel);//�˴���ת������������ֵ,
                        CurvePointF2[i] = new PointF(keys, values);

                        VideoValue _VideoValue = new VideoValue(i, (float)(LVP[indexparts].DrawDatalstPara[0][i ]));
                        LVP[indexparts].videoValue[i] = _VideoValue;
                        _VideoValue = new VideoValue(i, (float)(LVP[indexparts].DrawDatalstPara[1][i]));
                        LVP[indexparts].videoValueb[i] = _VideoValue;
                    }


                }


                g.FillRegion(mbrush /*UnderBrush*/, new Region(m_gp));
                g.DrawLines(CurvePen, CurvePointF);
                g.DrawLines(CurvePen2, CurvePointF2);
            }
            else
            {
                LVP[indexparts].videoValue.Clear();
                for (int i = LVP[indexparts].Sindex; i < LVP[indexparts].DisplayLen(); i++)  //ֻ��һ�����ݣ�ֻ����һ��
                {
                    keys = unitv * i + XSpace + this.lspan;
                    // if (LVP[indexparts].DrawDatalstPara[0][i] > 0)
                    {
                        values = SumHeght(indexparts) + (LVP[indexparts].y_zeroHeght) - (float)(LVP[indexparts].DrawDatalstPara[0][i] / LVP[indexparts].y_ValuePerPixel);//�˴���ת������������ֵ,
                        CurvePointF[i] = new PointF(keys, values);
                    }
                    VideoValue _VideoValue = new VideoValue(i, (float)(LVP[indexparts].DrawDatalstPara[0][i]));
                    LVP[indexparts].videoValue[i] = _VideoValue;
                }
                g.FillRegion(mbrush /*UnderBrush*/, new Region(m_gp));
                g.DrawLines(CurvePen, CurvePointF);
            }
            mbrush.Dispose();
            CurvePen.Dispose();
            CurvePen2.Dispose();
        }
        
        #endregion

        #region ϵͳ�Լ�����
        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
       public void SaveBmp()
       {
         //  Bitmap bmp = new Bitmap(this.ChartPanel.Width, this.ChartPanel.Height);
           this.ChartPanel.DrawToBitmap(imgTemp,new Rectangle(0,0,this.ChartPanel.Width, this.ChartPanel.Height));
         //  bmp.Save("c:\\aa.jpg");
           imgTemp.Save("c:\\aa.jpg");
       }
        /// <summary>
        /// ����ͼ������
        /// </summary>
		private void RecalculateSize()
		{
			if ((CurrentSize.Height != 0) && (CurrentSize.Width != 0)) //avoid divide by 0
			{
				g.Dispose();
                //imgTemp = new Bitmap(200, 200, ChartPanel.CreateGraphics());
                //g = Graphics.FromImage(imgTemp);
                g = ChartPanel.CreateGraphics();
				DrawChart();
			}
		}
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ChartPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ChartPanel
            // 
            this.ChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartPanel.Location = new System.Drawing.Point(0, 0);
            this.ChartPanel.Name = "ChartPanel";
            this.ChartPanel.Size = new System.Drawing.Size(289, 93);
            this.ChartPanel.TabIndex = 0;
            this.ChartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartPanel_Paint);
            this.ChartPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChartPanel_MouseDown);
            this.ChartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChartPanel_MouseMove);
            this.ChartPanel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ChartPanel_PreviewKeyDown);
            // 
            // MCADLine
            // 
            this.Controls.Add(this.ChartPanel);
            this.Name = "MCADLine";
            this.Size = new System.Drawing.Size(289, 93);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MCADLine_PreviewKeyDown);
            this.ResumeLayout(false);

		}
		#endregion
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (ChartPanel != null)
			{
				if ((Size.Height == 0) || (Size.Width == 0))	return;
				if ((CurrentSize.Height == 0) && (CurrentSize.Width == 0))
				{
					CurrentSize = Size;
					return;
				}
				RecalculateSize();
				CurrentSize = Size;
			}
		} 
		private void ChartPanel_Paint(object sender, PaintEventArgs e)
		{
			if (ChartPanel != null)
				OnResize(new EventArgs());
        }
        #endregion 
        private int Mousex = -1;
        private int OldMouseX = -1;
       /// <summary>
       /// 
       /// </summary>
        private float MouseLocation = -1f;
        private void ChartPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (exd)
            {
                if ((e.X >= XSpace + this.lspan) && (e.X <= (this.Width - this.rspan)))
                {
                    MouseLocation = 1f * (e.X - XSpace - this.lspan) / (this.Width - this.rspan - innerbar - this.lspan);
                    Mousex = e.X;
                    OldMouseX = e.X;
                    this.Refresh();
                    Mousex = -1;
                }
            }
            else
            {
               
            }
        }

        private void ChartPanel_MouseMove(object sender, MouseEventArgs e)
        {
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool br = false;
            // MessageBox.Show( keyData.ToString());
            switch (keyData)
            {//�������
                case Keys.Right:
                    //this.Uk++;
                    br = true;
                    break;
                case Keys.Left:
                    //this.Uk--;
                    br = true;
                    break;

            }
            if (br)
            {
                this.Focus();
                return br;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void ChartPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            bool bm = false; 
            switch (e.KeyData)
            {
                case Keys.Up:
                    //point.Y -= 2;
                    break;
                case Keys.Down:
                    //point.Y += 2;
                    break;
                case Keys.Right:
                    OldMouseX += 1;
                    bm = true;
                    break;
                case Keys.Left:
                    OldMouseX -= 1;
                    bm = true;
                    break;
                case Keys.Escape:
                    //this.Close();
                    break;
                default: break;
            }
            if (bm)
            {


                if (exd)
                {
                    if ((OldMouseX >= XSpace + this.lspan) && (OldMouseX <= (this.Width - this.rspan)))
                    {
                        MouseLocation = 1f * (OldMouseX - XSpace - this.lspan) / (this.Width - this.rspan - innerbar - this.lspan);
                        Mousex = OldMouseX;
                        this.Refresh();
                        Mousex = -1;
                    }
                }
            }
          //  this.Location = point;
        }

        private void MCADLine_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            bool bm = false;
            switch (e.KeyData)
            {
                case Keys.Up:
                    //point.Y -= 2;
                    break;
                case Keys.Down:
                    //point.Y += 2;
                    break;
                case Keys.Right:
                    OldMouseX += 1;
                    bm = true;
                    break;
                case Keys.Left:
                    OldMouseX -= 1;
                    bm = true;
                    break;
                case Keys.Escape:
                    //this.Close();
                    break;
                default: break;
            }
            if (bm)
            {


                if (exd)
                {
                    if ((OldMouseX >= XSpace + this.lspan) && (OldMouseX <= (this.Width - this.rspan)))
                    {
                        MouseLocation = 1f * (OldMouseX - XSpace - this.lspan) / (this.Width - this.rspan - innerbar - this.lspan);
                        Mousex = OldMouseX;
                        this.Refresh();
                        Mousex = -1;
                    }
                }
            }
        }
    }	
	
}