using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MovieProject.Model.Dtos.Users;
using MovieProject.Service.Abstracts;
using MovieProject.Service.BusinessRules.Users;
using MovieProject.Service.Constants.Users;

namespace MovieProject.Service.Concretes;
public sealed class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public AuthService(IUserService userService, UserBusinessRules userBusinessRules, IMapper mapper, IJwtService jwtService)
    {
        _userService = userService;
        _userBusinessRules = userBusinessRules;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<AccessToken> LoginAsync(LoginRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        // todo: Email kontrolü yap
        await _userBusinessRules.SearchByEmailAsync(requestDto.Email);

        UserResponseDto user = await _userService.GetByEmailAsync(requestDto.Email,cancellationToken);


        var verifyPassword = HashingHelper
            .VerifyPasswordHash(requestDto.Password, user.PasswordHash, user.PasswordSalt);

        if (!verifyPassword)
            throw new BusinessException(UserMessages.PasswordIsWrong);


         User userWithToken = _mapper.Map<User>(user);
            
         AccessToken accessToken = await  _jwtService.CreateAccessTokenAsync(userWithToken);

        return accessToken;

        // Parola eşleşiyor mu ? 

    }

    public async Task<AccessToken> RegisterAsync(RegisterRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        User user = _mapper.Map<User>(requestDto);

        var hashResult = HashingHelper.CreatePasswordHash(requestDto.Password);

        user.PasswordHash = hashResult.passwordHash;
        user.PasswordSalt = hashResult.passwordSalt;
        user.Status = true;

      UserResponseDto created =  await _userService.AddAsync(user);

        User userWithToken = _mapper.Map<User>(created);

        AccessToken accessToken = await _jwtService.CreateAccessTokenAsync(userWithToken);

        return accessToken;

    }

    public async Task<AccessToken> UpdateUserAsync(int id,UserUpdateRequestDto userUpdateRequestDto)
    {
        await _userBusinessRules.UserIsPresent(id);

        UserResponseDto userResponse = await _userService.GetByIdAsync(id);

        User userEntity = _mapper.Map<User>(userResponse);

        User updated =  _mapper.Map(userUpdateRequestDto,userEntity);

        await _userService.UpdateAsync(updated);

        AccessToken accessToken = await _jwtService.CreateAccessTokenAsync(updated);

        return accessToken;
    }
}
