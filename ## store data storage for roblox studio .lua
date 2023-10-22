## store data storage for roblox studio once purchased the store it will save all data in the game eg the player that Name
everytime players re joins a server they will open the store and all data will be there eg clothes and shop name 

local MarketplaceService = game:GetService("MarketplaceService")
local DataStoreService = game:GetService("DataStoreService")
local InsertService = game:GetService("InsertService")
local RepStorage = game:GetService("ReplicatedStorage")
local Players = game:GetService("Players")

local DataStore = DataStoreService:GetDataStore("PlrTestStoreIDs")

local RentStoreEvent = RepStorage.RentStore
local EditEvent = RepStorage.EditMannequin

local GamepassID = 90150094 --Gamepass Id here

local function LoadData (Plr)
	
	local Key = Plr.UserId
	local Retry = 0
	local Success
	local Data

	repeat
		Retry = Retry + 1
		Success,Data = pcall(function()	
			return DataStore:GetAsync(Key)		
		end)
		if not Success then
			wait(1)
		end
	until Retry == 3 or Success

	if Success then
		if Data then
			for i, v in pairs(Data) do

				local Mannnequin = Instance.new("Folder")
				Mannnequin.Name = i

				local Shirt = Instance.new("StringValue")
				Shirt.Name = "Shirt"
				Shirt.Value = v.Shirt
				Shirt.Parent = Mannnequin

				local Pants = Instance.new("StringValue")
				Pants.Name = "Pants"
				Pants.Value = v.Pants
				Pants.Parent = Mannnequin

				Mannnequin.Parent = Plr.StoreIDs
			end
		end
	else
		print(Success)
	end
end


Players.PlayerAdded:Connect(function(Plr)
	
	local leaderstats = Instance.new("Model")
	leaderstats.Name = "leaderstats"
	leaderstats.Parent = Plr   
	

	local OwnsGamepass = Instance.new("BoolValue")
	OwnsGamepass.Name = "OwnsGamepass"
	OwnsGamepass.Value = MarketplaceService:UserOwnsGamePassAsync(Plr.UserId,GamepassID)
	OwnsGamepass.Parent = Plr
	
	local OwnsStore = Instance.new("BoolValue")
	OwnsStore.Name = "OwnsStore"
	OwnsStore.Value = false 
	OwnsStore.Parent = Plr
	
	local StoreIDs = Instance.new("Folder")
	StoreIDs.Name = "StoreIDs"
	StoreIDs.Parent = Plr
	
	LoadData(Plr)

end)

local function LoadStore (Plr,Store)
	for i,v in pairs(Plr.StoreIDs:GetChildren()) do
		local Mannequin = Store.MannequinsFolder[v.Name]

		if v.Shirt.Value ~= "" then
			Mannequin.shirtValue.Value = v.Shirt.Value

			local Model = InsertService:LoadAsset(v.Shirt.Value) 
			Mannequin.Shirt:Destroy()
			Model.Shirt.Parent = Mannequin
		end

		if v.Pants.Value ~= "" then
			Mannequin.pantValue.Value = v.Pants.Value

			local Model = InsertService:LoadAsset(v.Pants.Value) 
			Mannequin.Pants:Destroy()
			Model.Pants.Parent = Mannequin
		end

	end
end


local function SetupStore(Plr,Store)
	if Plr.OwnsStore.Value == false  then	
		local Frame = Store.StoreName.SurfaceGui.Frame
		Frame.StoreName.Text = Plr.Name.."'s Store"
		
		Store:SetAttribute("Owner", Plr.Name)
		Plr.OwnsStore.Value = true 
		
		Store.TouchPart.SurfaceGui.Enabled = false
		Store.TouchPart.Transparency = 1
		Store.TouchPart.CanCollide = false
		Store.TouchPart.ProximityPrompt.Enabled = false
		
		LoadStore(Plr,Store)
		
		return true
	end
end

local function EditMannequin(Plr,ShirtID,PantsID,Mannequin)
	local MannnequinFolder
	
	if Plr.StoreIDs:FindFirstChild(Mannequin.Name) then
		MannnequinFolder = Plr.StoreIDs[Mannequin.Name]
	else
		MannnequinFolder = Instance.new("Folder")
		MannnequinFolder.Name = Mannequin.Name
		MannnequinFolder.Parent = Plr.StoreIDs
		
		local Shirt = Instance.new("StringValue")
		Shirt.Name = "Shirt"
		Shirt.Parent = MannnequinFolder
		
		local Pants = Instance.new("StringValue")
		Pants.Name = "Pants"
		Pants.Parent = MannnequinFolder
	end
	
	if ShirtID ~= "" then
		local Model = InsertService:LoadAsset(ShirtID) 
		
		if Model:FindFirstChild("Shirt") then
			Mannequin.shirtValue.Value = ShirtID
			Mannequin.Shirt:Destroy()
			Model.Shirt.Parent = Mannequin
			
			MannnequinFolder.Shirt.Value = ShirtID
		else
			print("Wrong Shirt ID!")
		end
	end
	
	if PantsID ~= "" then
		local Model = InsertService:LoadAsset(PantsID) 
		
		if Model:FindFirstChild("Pants") then
			Mannequin.pantValue.Value = PantsID
			Mannequin.Pants:Destroy()
			Model.Pants.Parent = Mannequin
			
			MannnequinFolder.Pants.Value = PantsID
		else
			print("Wrong Pants ID!")
		end
	end
end

RentStoreEvent.OnServerInvoke = SetupStore
EditEvent.OnServerEvent:Connect(EditMannequin)


local function SaveData (Plr)
	
	local StoreIDs = {}

	for i, v in pairs(Plr.StoreIDs:GetChildren()) do
		StoreIDs[v.Name] = {["Shirt"] = v.Shirt.Value,["Pants"] = v.Pants.Value}
	end
	
	print(StoreIDs)

	local Retry = 0
	local Success
	local Message 

	repeat
		Retry = Retry + 1

		Success, Message = pcall(function()	
			DataStore:SetAsync(Plr.UserId,StoreIDs)					
		end)

		if not Success then 
			print(Message)
			wait(1)
		end	

	until Success or Retry == 3	

	if Success then
		print("Success")
	else
		print(Message)
	end
end

local function ResetStore(Plr)
	for i,Store in pairs(workspace.RentableStores:GetChildren()) do
		if Store:GetAttribute("Owner") == Plr.Name then
			Store.TouchPart.Transparency = 0.75
			Store.TouchPart.SurfaceGui.Enabled = true
			Store.TouchPart.CanCollide = true
			Store.TouchPart.ProximityPrompt.Enabled = true
			
			Store:SetAttribute("Owner", "")
			
			local Frame = Store.StoreName.SurfaceGui.Frame
			Frame.StoreName.Text = "Username's Store"
			
			for k,v in pairs(Store.MannequinsFolder:GetChildren()) do
				if v:IsA("Model") then
					v.shirtValue.Value = 0
					v.pantValue.Value = 0
					
					v.Shirt.ShirtTemplate = ""
					v.Pants.PantsTemplate = ""
				end
			end
		end
	end
end

game:BindToClose(function()
	for i, Plr in pairs(Players:GetPlayers()) do
		SaveData(Plr)
	end
end)

Players.PlayerRemoving:Connect(function(Plr)
	SaveData(Plr)
	ResetStore(Plr)
end)

