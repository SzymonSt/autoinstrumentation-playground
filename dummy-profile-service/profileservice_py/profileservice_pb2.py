# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: profileservice.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x14profileservice.proto\x12\x07profile\"2\n\x07Profile\x12\n\n\x02id\x18\x01 \x01(\t\x12\x0c\n\x04name\x18\x02 \x01(\t\x12\r\n\x05\x65mail\x18\x03 \x01(\t\"\x1f\n\x11GetProfileRequest\x12\n\n\x02id\x18\x01 \x01(\t\"7\n\x12GetProfileResponse\x12!\n\x07profile\x18\x01 \x01(\x0b\x32\x10.profile.Profile\"0\n\x11SetProfileRequest\x12\x0c\n\x04name\x18\x01 \x01(\t\x12\r\n\x05\x65mail\x18\x02 \x01(\t\"7\n\x12SetProfileResponse\x12!\n\x07profile\x18\x01 \x01(\x0b\x32\x10.profile.Profile2\x9e\x01\n\x0eProfileService\x12\x45\n\nGetProfile\x12\x1a.profile.GetProfileRequest\x1a\x1b.profile.GetProfileResponse\x12\x45\n\nSetProfile\x12\x1a.profile.SetProfileRequest\x1a\x1b.profile.SetProfileResponseB\x87\x01\n\x1e\x63om.szymonst.profileservice.v1B\x13ProfileServiceProtoZPgithub.com/SzymonSt/autoinstrumentation-playground/dummy-proto/profileservice/v1b\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'profileservice_pb2', _globals)
if _descriptor._USE_C_DESCRIPTORS == False:

  DESCRIPTOR._options = None
  DESCRIPTOR._serialized_options = b'\n\036com.szymonst.profileservice.v1B\023ProfileServiceProtoZPgithub.com/SzymonSt/autoinstrumentation-playground/dummy-proto/profileservice/v1'
  _globals['_PROFILE']._serialized_start=33
  _globals['_PROFILE']._serialized_end=83
  _globals['_GETPROFILEREQUEST']._serialized_start=85
  _globals['_GETPROFILEREQUEST']._serialized_end=116
  _globals['_GETPROFILERESPONSE']._serialized_start=118
  _globals['_GETPROFILERESPONSE']._serialized_end=173
  _globals['_SETPROFILEREQUEST']._serialized_start=175
  _globals['_SETPROFILEREQUEST']._serialized_end=223
  _globals['_SETPROFILERESPONSE']._serialized_start=225
  _globals['_SETPROFILERESPONSE']._serialized_end=280
  _globals['_PROFILESERVICE']._serialized_start=283
  _globals['_PROFILESERVICE']._serialized_end=441
# @@protoc_insertion_point(module_scope)
