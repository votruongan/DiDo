using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml;

public class LanguageController : MonoBehaviour {
	private int NextLangID = 0;
	public string[] LangList;
	public Text[] Texts;
	public RawImage LangIcon;
	public Texture[] LanguageIcons;
	public string LangDirectory;
	public string SAPath;

	void Start () {
		LangDirectory = Path.Combine(Application.persistentDataPath,"Texts");
		if (!(Directory.Exists(LangDirectory))) {
			SAPath = Path.Combine (Application.streamingAssetsPath, "Texts");
			Directory.CreateDirectory (LangDirectory);
			string xmlpath ="Language_English.xml";
			StartCoroutine(WriteXML (xmlpath));
			xmlpath = "Language_Vietnamese.xml";
			StartCoroutine(WriteXML (xmlpath));

		}
	}

	void toggleLangID(){
		NextLangID = (NextLangID == 1)?(0):(1);
	}

	public void UpdateLanguage(){
		toggleLangID ();
		Debug.Log("<><><><><><><><><><><> Update Language " + NextLangID);
		if (LangIcon != null) {
			LangIcon.texture = LanguageIcons [NextLangID];			
		}
		Debug.Log (LangList.Length);
		foreach (string lan in LangList) {
			Debug.Log (lan);
		}
		XmlDocument TextsData = new XmlDocument ();
		TextsData.Load(Path.Combine(LangDirectory,("Language_"+LangList[NextLangID]+".xml")));
		Debug.Log (TextsData.InnerText);
		XmlNode TextContent =  TextsData.SelectSingleNode("/Texts");
		foreach (Text txt in Texts) {
			try {		
				txt.text = TextContent.SelectSingleNode (txt.name).InnerText;		
				Debug.Log (txt.text);		
			} catch (System.Exception ex) {
				Debug.Log (txt.name + " FAILED: " + ex.Message);
			}
		}
		LangIcon.texture = LanguageIcons [NextLangID];
	}

	IEnumerator WriteXML(string xmlname){
		string xmlpath = Path.Combine (SAPath,xmlname);
		Debug.Log (xmlpath);
		//Load xml from Streaming Assets
		WWW loadDB = new WWW(xmlpath);
		yield return loadDB;
		Debug.Log (loadDB.isDone);
		Debug.Log (loadDB.text);
		Debug.Log (loadDB.text);
		// then save to Application.persistentDataPath
		File.WriteAllBytes(Path.Combine(LangDirectory,xmlname), loadDB.bytes);
		/*
		Directory.CreateDirectory (LangDirectory + @"\Texts");
		try{
			File.WriteAllText (LangDirectory + @"\Texts\Language_English.xml",
			"<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
			"\n<Texts>\n\t<App_Slogan>Discover the Dorm</App_Slogan>" +
			"\n\t<Butt_Search>Search for features</Butt_Search>\n\t" +
			"<Butt_Sect_A>SECTOR A</Butt_Sect_A>\n\t<Butt_Sect_B>SECTOR B" +
			"</Butt_Sect_B>\n\t<Change_Lang>TIẾNG VIỆT</Change_Lang>\n</Texts>", System.Text.Encoding.UTF8);
		}
		catch(IOException e){
			Debug.Log(e);
		}
		try{
			File.WriteAllText (LangDirectory + @"\Texts\Language_Vietnamese.xml",
			"<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n<Texts>\n" +
			"\t<App_Slogan>Khám phá Kí túc xá</App_Slogan>\n" +
			"\t<Butt_Search>Tìm kiếm tiện ích</Butt_Search>\n" +
			"\t<Butt_Sect_A>KHU A</Butt_Sect_A>\n\t<Butt_Sect_B>" +
			"KHU B</Butt_Sect_B>\n\t<Change_Lang>ENGLISH</Change_Lang>\n</Texts>", System.Text.Encoding.UTF8);
		}
		catch(IOException e){
			Debug.Log(e);
		}*/
	}
}
