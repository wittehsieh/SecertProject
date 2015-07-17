using System.Collections;

public class RangeValue
{
	public delegate void onValueChangeDelegate();

	public onValueChangeDelegate onReachMinEvent = delegate{ };
	public onValueChangeDelegate onReachMaxEvent = delegate{ };
	public onValueChangeDelegate onDecrease = delegate{ };
	public onValueChangeDelegate onIncrease = delegate{ };
	public onValueChangeDelegate onValueChangeWhenRangeChange = delegate { };
	public onValueChangeDelegate onCurValueChangedEvent = delegate { };

	protected float _min;
	protected float _max;
	protected float _cur;
	protected float _delta;

	#region Properties
	public float Min
	{
		get
		{
			return _min;
		}
		set
		{
			if(value > _max)
			{
				_min = _max;
				_max = value;
			}
			else
			{
				_min = value;
			}

			if(_cur < _min)
			{
				_cur = _min;
				onValueChangeWhenRangeChange();
			}
		}
	}
	public float Max
	{
		get
		{
			return _max;
		}
		set
		{
			if(value < _min)
			{
				_max = _min;
				_min = value;
			}
			else
			{
				_max = value;
			}

			if(_cur >= _max)
			{
				_cur = _max;
				onValueChangeWhenRangeChange();
			}
		}
	}
	public float Cur
	{
		get
		{
			return _cur;
		}
		set
		{
			_delta = value - _cur;

			if(_delta > 0)
			{
				onIncrease();
			}
			else if(_delta < 0)
			{
				onDecrease();
			}

			_cur = value;

			if(_cur <= _min)
			{
				_cur = _min;
				onReachMinEvent();
			}
			else if(_cur >= _max)
			{
				_cur = _max;
				onReachMaxEvent();
			}
			else
			{
				onCurValueChangedEvent();
			}
		}
	}
	#endregion

	public RangeValue(float min, float max, float cur)
	{
		_min = min;
		_max = max;
		_cur = cur;
	}
}
