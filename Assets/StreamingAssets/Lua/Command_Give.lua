Util = require('Util')

function RunCommand( inputs )
	if table.getn(inputs) > 2 or table.getn(inputs) < 2 then
		return false
	end

	item = Util.GetItem()
	if item == nil then
		return false
	end

	
end