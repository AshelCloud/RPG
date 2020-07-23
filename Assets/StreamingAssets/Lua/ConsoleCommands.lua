function give( code )
	code = tonumber(code)

	item = Console.GetItem(code)
	Console.Player.AddItem(item)

	Console.Log("Giving Player" .. item.Name )

	return true
end

function speed( amount )
	amount = tonumber(amount)
	Console.Player.moveSpeed = amount
	Console.Log("Player Speed: " .. tostring(amount))
end

function jump_power( amount )
	amount = tonumber(amount)
	Console.Player.jumpForce = amount
	Console.Log("Player JumpForce: " .. tostring(amount))
end

function quit( )
	Console.Quit()
end