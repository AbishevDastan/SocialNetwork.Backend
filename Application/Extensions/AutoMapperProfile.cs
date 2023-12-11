using Application.UseCases.Follow;
using Application.UseCases.Post;
using Application.UseCases.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthenticateUserDto>();
            CreateMap<AuthenticateUserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
            CreateMap<Post, AddPostDto>();
            CreateMap<AddPostDto, Post>();
            CreateMap<PostDto, AddPostDto>();
            CreateMap<AddPostDto, PostDto>();
            CreateMap<Post, UpdatePostDto>();
            CreateMap<UpdatePostDto, Post>();
            CreateMap<PostDto, UpdatePostDto>();
            CreateMap<UpdatePostDto, PostDto>();

            CreateMap<PostReaction, PostReactionDto>();
            CreateMap<PostReactionDto, PostReaction>();

            CreateMap<Follow, FollowDto>();
            CreateMap<FollowDto, Follow>();
        }
    }
}
