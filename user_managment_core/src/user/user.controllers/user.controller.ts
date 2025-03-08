import { Controller, Get, Post, Body, Patch, Param, Delete, Inject, Injectable, Scope } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { ApiOperation } from '@nestjs/swagger';
import {  IuserWriteService } from '../contracts/user.repository.Interface';




@Injectable({ scope: Scope.DEFAULT }) 
@Controller('registracion')
export class RegistracionController {
  constructor(@Inject("IuserWriteService") private readonly UserWriteService: IuserWriteService) { }



  @Post('register')
  async register(@Body() body: CreateRegistracionDto) {

    return await this.UserWriteService.registerService(body);

  }


  @Patch('update/:id')
  async update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    return await this.UserWriteService.updateUserService(id, updateRegistracionDto);
  }


  @Delete('delete/:email')
  async remove(@Param('email') email: string) {
    return await this.UserWriteService.deleteByemailService(email);
  }
  
}