using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
	public static DataManager instance;

	public SaveData nowData = new SaveData();

	string path;
	string filename = "save";

	//�����͸� ���ϰ� ����ϱ� ���� �̱������� ����
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			path = Application.persistentDataPath + "/";
		}

		else if (instance != this)
		{
			Destroy(instance.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//Ư�� ������ �����ϰ� ������ DataManager.instance.nowData.name = object.text
	//���̺굥���� �ҽ� DataManager.instance.SaveData();
	public void SaveData()
	{
		string data = JsonUtility.ToJson(nowData);

		File.WriteAllText(path + filename, data);
	}

	//�����͸� �ҷ��ö� ����. ���� json�� ����Ǿ��ִ� ������ �о� nowData�� �ִ´�
	public void LoadData()
	{
		string data = File.ReadAllText(path + filename);

		nowData = JsonUtility.FromJson<SaveData>(data);
	}

	//����Ǿ��ִ� �����Ͱ� �ִ��� üũ
	public bool DataCheck()
	{
		if (File.Exists(path + filename) == false)
		{
			Debug.Log("���̺� ������ ���� ���� ������ ����ϴ�");

			// Some Code...

			return false;
		}

		return true;
	}

}

[System.Serializable]
public class SaveData
{
	//���� => �̸�, ü��, ���뼺, ��ȸ��, �ڽŰ�, ����,��, ��¥, �ð�, ��,��Ʈ��������
	public string name;
    public int hp;
    public int flex;
    public int social;
    public int conf;
	public int intel;
	public int money;
	public int day;
	public int hour;
	public int minute;
	public int stress;
}
