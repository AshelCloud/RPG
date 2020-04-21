function give( code )
	code = tonumber(code)
	
	item = Console.GetItem(code)
	Console.Player.AddItem(item)

	return true
end