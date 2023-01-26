﻿namespace WebApi.DAL.Entities;

public class UserToken
{
	public int Id { get; set; }
	public string RefreshToken { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
}
